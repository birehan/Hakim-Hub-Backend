using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Addresses.CQRS.Commands;
using Application.Features.Addresses.CQRS.Handlers;
using Application.Features.Addresses.DTOs;
using Application.Features.Addresses.DTOs.Validators;
using Application.Contracts.Persistence;
using Application.UnitTest.Mocks;
using Application.Responses;
using AutoMapper;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTest.Addresses.Commands
{
    public class CreateAddressCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly CreateAddressCommandHandler handler;

        public CreateAddressCommandHandlerTests()
        {
            unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            mapperMock = new Mock<IMapper>();
            handler = new CreateAddressCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidAddress_ReturnsSuccessWithCorrectValues()
        {
            Console.WriteLine("✨ Executing Handle_ValidAddress_ReturnsSuccessWithCorrectValues...");

            // Arrange
            var createAddressDto = new CreateAddressDto
            {
                Country = "Sample Country",
                Region = "Sample Region",
                Zone = "Sample Zone",
                Woreda = "Sample Woreda",
                City = "Sample City",
                SubCity = "Sample SubCity",
                Longitude = 1.23,
                Latitude = 4.56,
                Summary = "Sample Summary",
                InstitutionId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            var createAddressCommand = new CreateAddressCommand
            {
                CreateAddressDto = createAddressDto
            };

            var addressEntity = new Address
            {
                Id = Guid.NewGuid()
            };

            mapperMock.Setup(m => m.Map<Address>(createAddressDto)).Returns(addressEntity);

            // Act
            var result = await handler.Handle(createAddressCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(addressEntity.Id);
            unitOfWorkMock.Verify(x => x.Save(), Times.Exactly(1));

            Console.WriteLine("✅ Handle_ValidAddress_ReturnsSuccessWithCorrectValues executed successfully.");
        }

        [Fact]
        public async Task Handle_InvalidAddress_ReturnsFailureWithErrorMessage()
        {
            Console.WriteLine("✨ Executing Handle_InvalidAddress_ReturnsFailureWithErrorMessage...");

            // Arrange
            var createAddressDto = new CreateAddressDto();

            var createAddressCommand = new CreateAddressCommand
            {
                CreateAddressDto = createAddressDto
            };

            // Act
            var result = await handler.Handle(createAddressCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe("Country is required.");
            unitOfWorkMock.Verify(u => u.AddressRepository.Add(It.IsAny<Address>()), Times.Never);
            unitOfWorkMock.Verify(u => u.Save(), Times.Never);
            mapperMock.Verify(m => m.Map<Address>(createAddressDto), Times.Never);

            Console.WriteLine("❌ Handle_InvalidAddress_ReturnsFailureWithErrorMessage executed successfully.");
        }
    }
}
