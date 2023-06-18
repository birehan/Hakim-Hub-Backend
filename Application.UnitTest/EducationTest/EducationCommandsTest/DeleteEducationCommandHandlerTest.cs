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
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;

namespace Application.UnitTest.EducationTest.EducationCommandsTest;

public class DeleteEducationCommandHandlerTest
{
    private  IMapper _mapper { get; set; }
    private Mock<IPhotoAccessor> _mockPhotoAccessor { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private DeleteEducationCommandHandler _handler { get; set; }
    private readonly CreateEducationDto createEducationDto;
    private CreateEducationCommandHandler _createHandler { get; set; }


    public DeleteEducationCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mockPhotoAccessor = new Mock<IPhotoAccessor>();

        _mapper = new MapperConfiguration(c =>
      {
          c.AddProfile<MappingProfile>();
      }).CreateMapper();

        _handler = new DeleteEducationCommandHandler(_mockUnitOfWork.Object, _mapper);

        createEducationDto = new CreateEducationDto()
        {
            Id = Guid.NewGuid(),
            EducationInstitution = "to be deleted University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            FieldOfStudy = "Oncology",
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            EducationInstitutionLogoId = "Oxford Campus"
        };

        _createHandler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper, _mockPhotoAccessor.Object);
    }


    // [Fact]
    // public async Task DeleteEducationValid()
    // {

    //     var photo = new PhotoUploadResult { PublicId = Guid.NewGuid().ToString(), Url = "photo-public-id" };
    //     _mockPhotoAccessor.Setup(pa => pa.AddPhoto(It.IsAny<IFormFile>())).ReturnsAsync(photo);

    //     createEducationDto.EducationInstitutionLogoId = photo.PublicId;

    //     var result = await _createHandler.Handle(new CreateEducationCommand() { createEducationDto = createEducationDto }, CancellationToken.None);
    //     var educationToBeDeleted = result.Value.Id;

    //     var num = (await _mockUnitOfWork.Object.EducationRepository.GetAll()).Count;
    //     (await _mockUnitOfWork.Object.EducationRepository.GetAll()).Count.ShouldBe(3);
    //     var resultAfterDeletion = await _handler.Handle(new DeleteEducationCommand() { Id = educationToBeDeleted }, CancellationToken.None);
    //     resultAfterDeletion.Value.ShouldBeEquivalentTo(educationToBeDeleted);
    //     resultAfterDeletion.Error.ShouldBeEquivalentTo("Education Deleted Successfully.");
    //     (await _mockUnitOfWork.Object.EducationRepository.GetAll()).Count.ShouldBe(2);
    //     Assert.IsType<Result<Guid?>>(resultAfterDeletion);
    // }


    // [Fact]
    // public async Task DeleteEducationInvalid()
    // {
    //     var result = await _handler.Handle(new DeleteEducationCommand() { Id = Guid.NewGuid() }, CancellationToken.None);
    //     result.Value.ShouldBeEquivalentTo(null);
    //     result.Error.ShouldBeEquivalentTo("Education Not Found.");
    //     Assert.IsType<Result<Guid?>>(result);
    // }
}
