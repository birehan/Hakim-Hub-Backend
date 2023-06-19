using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.DoctorProfiles.CQRS.Handlers;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Moq;
using Shouldly;

namespace Application.UnitTest.DoctorProfiles.Queries
{
    public class GetDoctorProfileListBySpecialityIdQueryHandlerTests
    {
         private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetDoctorProfileListBySpecialityIdQueryHandler _handler;

        public GetDoctorProfileListBySpecialityIdQueryHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetDoctorProfileListBySpecialityIdQueryHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsDoctorProfileDto()
        {
            // Arrange
            var query = new GetDoctorProfileListBySpecialityIdQuery {SpecialityId= Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3303") };
            var expectedoctorProfile = new  List<DoctorProfileDto>
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
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<DoctorProfileDto>>>();
            result.Value.ShouldNotBeNull();
            for(var i = 0;i< result.Value.Count;i++ ){
                result.Value[i].FullName = expectedoctorProfile[i].FullName;
                result.Value[i].Id = expectedoctorProfile[i].Id;
                result.Value[i].photoId = expectedoctorProfile[i].photoId;
                result.Value[i].CareerStartTime = expectedoctorProfile[i].CareerStartTime;
                result.Value[i].MainInstitutionId = expectedoctorProfile[i].MainInstitutionId;
            }
        }
    }
}