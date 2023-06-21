using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.DoctorProfiles.CQRS.Handlers;
using Application.Profiles;
using Domain;
using static Domain.DoctorProfile;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Shouldly;
using Application.Responses;
using Application.Features.DoctorProfiles.DTOs;
using Application.UnitTest.Mocks;


namespace Application.UnitTest.DoctorProfiles.Queries
{
    public class GetDoctorProfileQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetDoctorProfileQueryHandler _handler;

        public GetDoctorProfileQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetDoctorProfileQueryHandler(_mockUow.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ExistingDoctorProfile_ReturnsSuccessResult()
        {
            // Arrange
            var doctorProfileId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3304");
            var expectedDoctorProfile = new DoctorProfileDto
            {
                Id = doctorProfileId,
                FullName = "Dr. Emily Johnson",
                YearsOfExperience = 0,
                MainInstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),

            };

            var query = new GetDoctorProfileQuery { Id = doctorProfileId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<DoctorProfileDto>>();
            result.Value.ShouldNotBeNull();
            result.Value.FullName.ShouldBe(expectedDoctorProfile.FullName);
            // result.Value.YearsOfExperience.ShouldBe(expectedDoctorProfile.YearsOfExperience);
            result.Value.MainInstitutionId.ShouldBe(expectedDoctorProfile.MainInstitutionId);
        }

        [Fact]
        public async Task Handle_NonExistingDoctorProfile_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingDoctorProfileId = Guid.NewGuid();

            var query = new GetDoctorProfileQuery { Id = nonExistingDoctorProfileId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Value.ShouldBeNull();
            result.ShouldBeOfType<Result<DoctorProfileDto>>();
        }
    }

}
