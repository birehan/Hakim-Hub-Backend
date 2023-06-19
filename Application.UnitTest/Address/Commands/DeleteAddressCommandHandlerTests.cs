using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.Contracts.Persistence;
using Application.Features.Addresses.CQRS.Commands;
using Application.Features.Addresses.CQRS.Handlers;
using Application.Responses;
using Application.UnitTest.Mocks;
using Domain;

namespace Application.UnitTest.Addresses.Commands
{
    public class DeleteAddressCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAddressRepository> _mockAddressRepository;
        private readonly DeleteAddressCommandHandler _handler;

        public DeleteAddressCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new DeleteAddressCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task DeleteAddress_ValidId_ShouldReturnSuccessResult()
        {
            // Arrange
            var addressId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var deleteCommand = new DeleteAddressCommand { Id = addressId };
            var addressToDelete = new Address { Id = addressId };
            // Act
            var result = await _handler.Handle(deleteCommand, CancellationToken.None);

            
            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Result<Guid>>();
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(addressId);
        }

        [Fact]
        public async Task DeleteAddress_InvalidId_ShouldReturnFailureResult()
        {
            // Arrange
            var addressId = Guid.NewGuid();
            var deleteCommand = new DeleteAddressCommand { Id = addressId };

            // Act
            var result = await _handler.Handle(deleteCommand, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Result<Guid>>();
            result.IsSuccess.ShouldBeFalse();
            result.Value.ShouldBe(Guid.Empty);
            result.Error.ShouldNotBeEmpty();
        }
    }
}
