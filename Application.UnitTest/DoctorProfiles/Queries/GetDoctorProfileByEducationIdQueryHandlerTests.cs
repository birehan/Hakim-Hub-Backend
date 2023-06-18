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
    public class GetDoctorProfileByEducationIdQueryHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetDoctorProfileByEducationIdQueryHandler _handler;

        public GetDoctorProfileByEducationIdQueryHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetDoctorProfileByEducationIdQueryHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsDoctorProfileDto()
        {
            // Arrange
            var query = new GetDoctorProfileByEducationIdQuery { EducationId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3303") };
            var expectedoctorProfile = new List<DoctorProfileDto>
            { new DoctorProfileDto{

                Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3304"),
                FullName = "Dr. Emily Johnson",
                CareerStartTime =DateTime.Parse("2022-06-10T09:15:26.533993Z"),
                MainInstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3305"),
            }
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            for (var i = 0; i < result.Value.Count; i++)
            {
                result.Value[i].FullName = expectedoctorProfile[i].FullName;
                result.Value[i].Id = expectedoctorProfile[i].Id;
                result.Value[i].photoId = expectedoctorProfile[i].photoId;
                result.Value[i].CareerStartTime = expectedoctorProfile[i].CareerStartTime;
                result.Value[i].MainInstitutionId = expectedoctorProfile[i].MainInstitutionId;
            }
        }

        [Fact]
        public async Task Handle_NonExistentId_ReturnsDoctorProfileDto()
        {
            // Arrange
            var query = new GetDoctorProfileByEducationIdQuery
            {

                EducationId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3309")

            };

            var expectedoctorProfile = new List<DoctorProfileDto>
            { new DoctorProfileDto{
                Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3308"),
                FullName = "Dr. Sophia Miller",
                photoId = "photo4",
                CareerStartTime = DateTime.Parse("2022-01-01T00:00:00Z"),
                MainInstitutionId = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3307"),
            }
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldBeNull();

        }
    }
}

