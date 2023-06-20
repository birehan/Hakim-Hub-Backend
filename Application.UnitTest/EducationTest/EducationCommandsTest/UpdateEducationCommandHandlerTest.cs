using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS;
using Application.Features.Educations.CQRS.Handlers;
using Application.Features.Educations.DTOs;
using Application.Interfaces;
using Application.Photos;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTest.EducationTest.EducationCommandsTest
{
    public class UpdateEducationCommandHandlerTest
    {
        private IMapper _mapper { get; set; }
        private Mock<IPhotoAccessor> _photoAccesor { get; set; }
        private readonly UpdateEducationCommandHandler _handler;
        private readonly CreateEducationDto createEducationDto;
        private CreateEducationCommandHandler _createHandler { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }


        public UpdateEducationCommandHandlerTest()
        {
            _photoAccesor = new Mock<IPhotoAccessor>();
            _mapper = new MapperConfiguration(c =>
      {
          c.AddProfile<MappingProfile>();
      }).CreateMapper();
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _handler = new UpdateEducationCommandHandler(_mockUnitOfWork.Object, _mapper, _photoAccesor.Object);
        createEducationDto = new CreateEducationDto()
        {
            Id = Guid.NewGuid(),
            EducationInstitution = "to be updated University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            FieldOfStudy = "Oncology",
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            // EducationInstitutionLogoId = "Oxford Campus"
        };

        _createHandler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper, _photoAccesor.Object);

        }

        [Fact]
        public async Task UpdateEducationValid()
        {
            var photo = new PhotoUploadResult { PublicId = Guid.NewGuid().ToString(), Url = "photo-public-id" };
            _photoAccesor.Setup(pa => pa.AddPhoto(It.IsAny<IFormFile>())).ReturnsAsync(photo);

            // createEducationDto.EducationInstitutionLogoUrl = photo.PublicUrl;

            var result = await _createHandler.Handle(new CreateEducationCommand() { createEducationDto = createEducationDto }, CancellationToken.None);
            var educationToBeUpdated = result.Value.Id;

            
            var updateEducationDto = new UpdateEducationDto
            {
                Id = educationToBeUpdated,
                EducationInstitution = "Updated University",
                StartYear = DateTime.Now.AddYears(-2),
                GraduationYear = DateTime.Now.AddYears(-1),
                Degree = "Master's",
                FieldOfStudy = "Psychiatry",
                DoctorId = Guid.NewGuid(),
                EducationInstitutionLogoPhotoId = "Updated Campus"
            };
           
            var updatedResult = await _handler.Handle(new UpdateEducationCommand(){updateEducationDto = updateEducationDto}, CancellationToken.None);
            
            updatedResult.Error.ShouldBeEquivalentTo("Education Updated Successfully.");
            Assert.IsType<Result<Unit?>>(updatedResult);
            result.IsSuccess.ShouldBeTrue();
            
        }

        [Fact]
        public async Task UpdateEducationInvalid_EducationNotFound()
        {

            var updateEducationDto = new UpdateEducationDto
            {
                Id = Guid.NewGuid(),
                EducationInstitution = "Updated University no to be found",
                StartYear = DateTime.Now.AddYears(-2),
                GraduationYear = DateTime.Now.AddYears(-1),
                Degree = "Master's",
                FieldOfStudy = "Psychiatry",
                DoctorId = Guid.NewGuid(),
                EducationInstitutionLogoPhotoId = "Updated Campus"
            };

            var result = await _handler.Handle(new UpdateEducationCommand(){updateEducationDto = updateEducationDto}, CancellationToken.None);

            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe("Education Not Found.");
            result.Value.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateEducationInvalid_ValidationFailed()
        {
            var photo = new PhotoUploadResult { PublicId = Guid.NewGuid().ToString(), Url = "photo-public-id" };
            _photoAccesor.Setup(pa => pa.AddPhoto(It.IsAny<IFormFile>())).ReturnsAsync(photo);

            // createEducationDto.EducationInstitutionLogoUrl = photo.Url;

            var result = await _createHandler.Handle(new CreateEducationCommand() { createEducationDto = createEducationDto }, CancellationToken.None);
            var educationToBeUpdated = result.Value.Id;

            var updateEducationDto = new UpdateEducationDto
            {
                Id = educationToBeUpdated,
                EducationInstitution = null,
                StartYear = DateTime.Now.AddYears(-2),
                GraduationYear = DateTime.Now.AddYears(-1),
                FieldOfStudy = null,
                Degree = "Master's",
                DoctorId = Guid.Empty,
                // EducationInstitutionLogoPhotoUrl = result.Value.EducationInstitutionLogoUrl
            };

            // Act
            var resultAfterFailedUpdate = await _handler.Handle(new UpdateEducationCommand(){updateEducationDto  = updateEducationDto }, CancellationToken.None);

            // Assert
            resultAfterFailedUpdate.IsSuccess.ShouldBeFalse();
            resultAfterFailedUpdate.Value.ShouldBeNull();
        }
    }
}
