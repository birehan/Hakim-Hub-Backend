using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Application.Contracts.Persistence;
using Application.Features.InstitutionProfiles.CQRS.Handlers;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Responses;
using Domain;
using Xunit;

namespace Application.UnitTest.InstitutionProfiles.Queries
{
    public class GetInstitutionProfileDetailQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetInstitutionProfileDetailQueryHandler _handler;

        public GetInstitutionProfileDetailQueryHandlerTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InstitutionProfile, InstitutionProfileDto>();
                // Add mappings for other types if needed
            }).CreateMapper();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetInstitutionProfileDetailQueryHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsInstitutionProfileDto()
        {
            // Arrange
            var institutionProfileId = Guid.NewGuid();
            var institutionProfile = new InstitutionProfile
            {
                Id = institutionProfileId,
                InstitutionName = "Sample Institution",
                // Set other properties accordingly
            };
            _mockUnitOfWork.Setup(uow => uow.InstitutionProfileRepository.GetPopulated(institutionProfileId))
                .ReturnsAsync(institutionProfile);

            var query = new GetInstitutionProfileDetailQuery { Id = institutionProfileId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<InstitutionProfileDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.IsType<InstitutionProfileDto>(result.Value);
            //TODO: Perform additional assertions
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.InstitutionProfileRepository.GetPopulated(nonExistingId))
                .ReturnsAsync((InstitutionProfile)null);

            var query = new GetInstitutionProfileDetailQuery { Id = nonExistingId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<InstitutionProfileDto>>(result);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
        }
    }
}
