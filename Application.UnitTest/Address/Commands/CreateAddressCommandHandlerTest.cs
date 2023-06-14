using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Addresses.CQRS.Commands;
using Application.Features.Addresses.CQRS.Handlers;
using Application.Features.Addresses.DTOs;
using Application.Features.Addresses.DTOs.Validators;
using Application.Contracts.Persistence;
using Application.Responses;
using AutoMapper;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace YourNamespace.UnitTest.Addresses.Commands
{
    public class CreateAddressCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidAddress_ReturnsSuccessWithCorrectValues()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            // Set up the CreateAddressDto with sample values
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
                InstitutionId = Guid.NewGuid()
            };

            // Set up the CreateAddressCommand with the CreateAddressDto
            var createAddressCommand = new CreateAddressCommand
            {
                CreateAddressDto = createAddressDto
            };

            // Set up the Address entity
            var addressEntity = new Address
            {
                Id = Guid.NewGuid(),
                // Set other properties based on createAddressDto
            };

            // Set up the expected result
            var expectedResult = Result<Guid>.Success(addressEntity.Id);

            // Set up the mock dependencies
            unitOfWorkMock.Setup(u => u.AddressRepository.Add(It.IsAny<Address>()));
            unitOfWorkMock.Setup(u => u.Save()).ReturnsAsync(1);
            mapperMock.Setup(m => m.Map<Address>(createAddressDto)).Returns(addressEntity);

            // Create an instance of the CreateAddressCommandHandler with the mock dependencies
            var handler = new CreateAddressCommandHandler(unitOfWorkMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(createAddressCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBe(expectedResult.IsSuccess);
            result.Value.ShouldBe(expectedResult.Value);
            unitOfWorkMock.Verify(u => u.AddressRepository.Add(It.IsAny<Address>()), Times.Once);
            unitOfWorkMock.Verify(u => u.Save(), Times.Once);
            mapperMock.Verify(m => m.Map<Address>(createAddressDto), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidAddress_ReturnsFailureWithErrorMessage()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            // Set up an invalid CreateAddressDto without required properties
            var createAddressDto = new CreateAddressDto();

            // Set up the CreateAddressCommand with the invalid CreateAddressDto
            var createAddressCommand = new CreateAddressCommand
            {
                CreateAddressDto = createAddressDto
            };

            // Set up the validator to return validation errors
            var validatorMock = new Mock<IValidator<CreateAddressDto>>();
            validatorMock
                .Setup(v => v.ValidateAsync(createAddressDto, CancellationToken.None))
                .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Country", "Country is required") }));

            // Create an instance of the CreateAddressCommandHandler with the mock dependencies
            var handler = new CreateAddressCommandHandler(unitOfWorkMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(createAddressCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe("Country is required.");
            unitOfWorkMock.Verify(u => u.AddressRepository.Add(It.IsAny<Address>()), Times.Never);
            unitOfWorkMock.Verify(u => u.Save(), Times.Never);
            mapperMock.Verify(m => m.Map<Address>(createAddressDto), Times.Never);
        }
    }
}
