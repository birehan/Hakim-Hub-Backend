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

namespace Application.UnitTest.DoctorProfiles.Queries
{
    public class FilterDoctorProfilesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly FilterDoctorProfilesQueryHandler _handler;

        public FilterDoctorProfilesQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new FilterDoctorProfilesQueryHandler(_mockUow.Object, _mapper);
        }


        [Fact]
        public async Task Handle_WithAllParameters_ReturnsFilteredDoctorProfiles()
        {
            // Arrange
            var query = new FilterDoctorProfilesQuery
            {
                SpecialityName = "Cardiology",
                InstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),
                CareerStartTime = DateTime.Parse("2022-06-10T09:15:26.533993Z"),
                EducationName = "Medical College"
            };

            var expectedDoctorProfiles = new List<DoctorProfileDto>
                {
                    new DoctorProfileDto
                    {
                        Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3304"),
                        FullName ="Dr. Emily Johnson",
                        photoId = "photo3",
                        MainInstitutionId =  Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),
                        CareerStartTime = DateTime.Parse("2022-06-10T09:15:26.533993Z")
                    }
                };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            var actualDoctorProfiles = result.Value;
            actualDoctorProfiles.ShouldNotBeEmpty();
            actualDoctorProfiles.Count.ShouldBe(expectedDoctorProfiles.Count);

            for (int i = 0; i < actualDoctorProfiles.Count; i++)
            {
                var actualProfile = actualDoctorProfiles[i];
                var expectedProfile = expectedDoctorProfiles[i];

                actualProfile.photoId.ShouldBe(expectedProfile.photoId);
                actualProfile.FullName.ShouldBe(expectedProfile.FullName);
                actualProfile.Id.ShouldBe(expectedProfile.Id);
                actualProfile.CareerStartTime.ShouldBe(expectedProfile.CareerStartTime);
                actualProfile.MainInstitutionId.ShouldBe(expectedProfile.MainInstitutionId);
            }
        }

        [Fact]
        public async Task Handle_WithNoParameters_ReturnsAllDoctorProfiles()
        {
            // Arrange
            var query = new FilterDoctorProfilesQuery();

            var expectedDoctorProfiles = new List<DoctorProfileDto> { };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldBeNull();


        }

        [Fact]
        public async Task Handle_WithSpecialityName_ReturnsFilteredDoctorProfiles()
        {
            // Arrange
            var query = new FilterDoctorProfilesQuery
            {
                InstitutionId = null,
                SpecialityName = "Internal Medicine",
                CareerStartTime = null,
                EducationName = null
            };

            var expectedDoctorProfiles = new List<DoctorProfileDto>
    {
        new DoctorProfileDto
        {
            Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"),
            FullName = "Dr. John Smith",
            photoId = "photo1",
            MainInstitutionId =  Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3302"),
            CareerStartTime =DateTime.Parse("2022-02-10T12:15:26.5339930+03:00")
        }
    };


            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            var actualDoctorProfiles = result.Value;
            actualDoctorProfiles.ShouldNotBeEmpty();
            actualDoctorProfiles.Count.ShouldBe(expectedDoctorProfiles.Count);

            for (int i = 0; i < actualDoctorProfiles.Count; i++)
            {
                var actualProfile = actualDoctorProfiles[i];
                var expectedProfile = expectedDoctorProfiles[i];

                actualProfile.photoId.ShouldBe(expectedProfile.photoId);
                actualProfile.FullName.ShouldBe(expectedProfile.FullName);
                actualProfile.Id.ShouldBe(expectedProfile.Id);
                actualProfile.CareerStartTime.ShouldBe(expectedProfile.CareerStartTime);
                actualProfile.MainInstitutionId.ShouldBe(expectedProfile.MainInstitutionId);
            }


        }

        [Fact]
        public async Task Handle_WithInstitutionId_ReturnsFilteredDoctorProfiles()
        {
            // Arrange
            var query = new FilterDoctorProfilesQuery
            {
                InstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305")

            };

            var expectedDoctorProfiles = new List<DoctorProfileDto>
                {
                    new DoctorProfileDto
                    {
                        Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3304"),
                        FullName = "Dr. Emily Johnson",
                        photoId = "photo3",
                        MainInstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),
                        CareerStartTime =DateTime.Parse("2022-06-10T09:15:26.533993Z")
                    }
                };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            var actualDoctorProfiles = result.Value;
            actualDoctorProfiles.ShouldNotBeEmpty();
            actualDoctorProfiles.Count.ShouldBe(expectedDoctorProfiles.Count);

            for (int i = 0; i < actualDoctorProfiles.Count; i++)
            {
                var actualProfile = actualDoctorProfiles[i];
                var expectedProfile = expectedDoctorProfiles[i];

                actualProfile.photoId.ShouldBe(expectedProfile.photoId);
                actualProfile.FullName.ShouldBe(expectedProfile.FullName);
                actualProfile.Id.ShouldBe(expectedProfile.Id);
                actualProfile.CareerStartTime.ShouldBe(expectedProfile.CareerStartTime);
                actualProfile.MainInstitutionId.ShouldBe(expectedProfile.MainInstitutionId);
            }
        }

        [Fact]
        public async Task Handle_WithCareerStartTime_ReturnsFilteredDoctorProfiles()
        {
            // Arrange
            var query = new FilterDoctorProfilesQuery
            {
                CareerStartTime = DateTime.Parse("2022-06-10T09:15:26.533993Z")
            };

            var expectedDoctorProfiles = new List<DoctorProfileDto>
                {
                    new DoctorProfileDto
                    {
                        Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3304"),
                        FullName = "Dr. Emily Johnson",
                        photoId = "photo3",
                        CareerStartTime =DateTime.Parse("2022-06-10T09:15:26.533993Z"),
                        MainInstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),
                }};



            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            var actualDoctorProfiles = result.Value;
            actualDoctorProfiles.ShouldNotBeEmpty();
            actualDoctorProfiles.Count.ShouldBe(expectedDoctorProfiles.Count);

            for (int i = 0; i < actualDoctorProfiles.Count; i++)
            {
                var actualProfile = actualDoctorProfiles[i];
                var expectedProfile = expectedDoctorProfiles[i];

                actualProfile.photoId.ShouldBe(expectedProfile.photoId);
                actualProfile.FullName.ShouldBe(expectedProfile.FullName);
                actualProfile.Id.ShouldBe(expectedProfile.Id);
                actualProfile.CareerStartTime.ShouldBe(expectedProfile.CareerStartTime);
                actualProfile.MainInstitutionId.ShouldBe(expectedProfile.MainInstitutionId);
            }

        }


    }
}