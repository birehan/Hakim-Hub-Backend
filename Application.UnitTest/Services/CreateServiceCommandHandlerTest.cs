using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Services.CQRS.Commands;
using Application.Features.Services.CQRS.Handlers;
using Application.Features.Services.DTOs;
using AutoMapper;
using Domain;
using FluentValidation;
using Moq;
using Xunit;

namespace Application.UnitTest.Services
{
    public class CreateServiceCommandHandlerTests
    {
        private readonly CreateServiceCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateServiceCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _handler = new CreateServiceCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidServiceDto_ReturnsSuccessResultWithServiceId()
        {
            // Arrange
            var command = new CreateServiceCommand
            {
                ServiceDto = new CreateServiceDto
                {
                    ServiceName = "Test Service",
                    ServiceDescription = "Test Description"
                }
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            var mapperResult = new Service { Id = Guid.NewGuid() };

            var validatorMock = new Mock<IValidator<CreateServiceDto>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(command.ServiceDto, CancellationToken.None))
                .ReturnsAsync(validationResult);

            _mapperMock
                .Setup(mapper => mapper.Map<Service>(command.ServiceDto))
                .Returns(mapperResult);

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.ServiceRepository.Add(mapperResult))
                .Verifiable();

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Save())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(mapperResult.Id, result.Value);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ServiceRepository.Add(mapperResult), Times.Once);
            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Save(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidServiceDto_ReturnsFailureResultWithError()
        {
            // Arrange
            var command = new CreateServiceCommand
            {
                ServiceDto = new CreateServiceDto
                {
                    ServiceName = "", // Invalid service name (empty)
                    ServiceDescription = "Test Description"
                }
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("ServiceName", "Service name is required."));

            var validatorMock = new Mock<IValidator<CreateServiceDto>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(command.ServiceDto, CancellationToken.None))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Service name is required.", result.Error);
        }
    }
}
