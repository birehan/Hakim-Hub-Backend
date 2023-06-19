using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.DoctorProfiles.CQRS.Handlers;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTest.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private GetDoctorProfileListQueryHandler _handler;
        public GetDoctorProfileListQueryHandlerTests()
        {
            _mockUow =  MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetDoctorProfileListQueryHandler(_mockUow.Object,_mapper);
        }

        [Fact]
        public async Task Handle_WithExistingDoctorProfiles_ReturnsSuccessfulResultWithProfiles()
        {
            // Arrange
            var Query = new GetDoctorProfileListQuery();

            // Act
            var result = await _handler.Handle(Query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
           result.Value.Count.ShouldBe(3);

        }

        [Fact]
        public async Task Handle_WithNoDoctorProfiles_ReturnsUnsuccessfulResultWithError()
        {
            // Arrange
            var emptyDoctorProfiles = new List<Domain.DoctorProfile>();
            _mockUow.Setup(uow => uow.DoctorProfileRepository.GetAll())
                .ReturnsAsync(emptyDoctorProfiles);

            var handler = new GetDoctorProfileListQueryHandler(_mockUow.Object, _mapper);
            var query = new GetDoctorProfileListQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No doctor profile", result.Error);
        }
    }
}
