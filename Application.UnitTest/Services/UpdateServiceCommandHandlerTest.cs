using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Services.CQRS.Commands;
using Application.Features.Services.CQRS.Handlers;
using Application.Features.Services.DTOs;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Moq;
using Xunit;

namespace Application.UnitTest.Services
{
    public class UpdateServiceCommandHandlerTests
    {
        private readonly UpdateServiceCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateServiceCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateServiceCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidServiceDto_ReturnsSuccessResult()
        {
            // Arrange
            var command = new UpdateServiceCommand
            {
                ServiceDto = new UpdateServiceDto
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "Updated Service",
                    ServiceDescription = "Updated Description"
                }
            };

            var validationResult = new FluentValidation.Results.ValidationResult();

            var validatorMock = new Mock<IValidator<UpdateServiceDto>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(command.ServiceDto, CancellationToken.None))
                .ReturnsAsync(validationResult);

            var service = new Service { Id = Guid.NewGuid() };

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.ServiceRepository.Get(command.ServiceDto.Id))
                .ReturnsAsync(service);

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.ServiceRepository.Update(service))
                .Verifiable();

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Save())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Unit.Value, result.Value);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Get(command.ServiceDto.Id), Times.Once);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Update(service), Times.Once);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Save(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidServiceDto_ReturnsFailureResult()
        {
            // Arrange
            var command = new UpdateServiceCommand
            {
                ServiceDto = new UpdateServiceDto
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "", // Invalid service name (empty)
                    ServiceDescription = "Updated Description"
                }
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("ServiceName", "Service name is required."));

            var validatorMock = new Mock<IValidator<UpdateServiceDto>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(command.ServiceDto, CancellationToken.None))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Service name is required.", result.Error);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Get(It.IsAny<Guid>()), Times.Never);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Update(It.IsAny<Service>()), Times.Never);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Save(), Times.Never);
        }

        [Fact]
        public async Task Handle_NonexistentServiceId_ReturnsNullResult()
        {
            // Arrange
            var command = new UpdateServiceCommand
            {
                ServiceDto = new UpdateServiceDto
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "Updated Service",
                    ServiceDescription = "Updated Description"
                }
            };

            var validationResult = new FluentValidation.Results.ValidationResult();

            var validatorMock = new Mock<IValidator<UpdateServiceDto>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(command.ServiceDto, CancellationToken.None))
                .ReturnsAsync(validationResult);

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.ServiceRepository.Get(command.ServiceDto.Id))
                .ReturnsAsync((Service)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Get(command.ServiceDto.Id), Times.Once);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Update(It.IsAny<Service>()), Times.Never);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Save(), Times.Never);
        }

        
    }
}
