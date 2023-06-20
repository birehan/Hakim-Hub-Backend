using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
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
    public class InstitutionProfileSearchQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly InstitutionProfileSearchQueryHandler _handler;

        public InstitutionProfileSearchQueryHandlerTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InstitutionProfile, InstitutionProfileDto>();
                // Add mappings for other types if needed
            }).CreateMapper();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new InstitutionProfileSearchQueryHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsInstitutionProfileDtoList()
        {
            // Arrange
            var query = new InstitutionProfileSearchQuery
            {
                ServiceNames = new List<string> {"Sample Service", "Sample Service 2", "Sample Service 3"},
                OperationYears = 5,
                OpenStatus = true
            };

            var institutionProfiles = new List<InstitutionProfile>
            {
                new InstitutionProfile
                {
                    Id = Guid.NewGuid(),
                    InstitutionName = "Institution 1"
                },
                new InstitutionProfile
                {
                    Id = Guid.NewGuid(),
                    InstitutionName = "Institution 2"
                }
            };

            _mockUnitOfWork.Setup(uow => uow.InstitutionProfileRepository.Search(query.ServiceNames, query.OperationYears, query.OpenStatus))
                .ReturnsAsync(institutionProfiles);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<List<InstitutionProfileDto>>>(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.IsType<List<InstitutionProfileDto>>(result.Value);
            Assert.Equal(institutionProfiles.Count, result.Value.Count);
            // Perform additional assertions if needed
        }

        [Fact]
        public async Task Handle_EmptyQuery_ReturnsNull()
        {
            // Arrange
            var query = new InstitutionProfileSearchQuery();
            var institutionProfiles = new List<InstitutionProfile>();

            _mockUnitOfWork.Setup(uow => uow.InstitutionProfileRepository.Search(query.ServiceNames, query.OperationYears, query.OpenStatus))
                .ReturnsAsync(institutionProfiles);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<List<InstitutionProfileDto>>>(result);
            Assert.True(result.IsSuccess);
        }
    }
}
