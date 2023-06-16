using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS;
using Application.Features.Educations.CQRS.Handlers;
using Application.Features.Educations.DTOs;
using Application.Interfaces;
using Application.Photos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;

namespace Application.UnitTest.EducationTest.EducationCommandsTest;

public class CreateEducationCommandHandlerTest
{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private readonly Mock<IPhotoAccessor> _mockPhotoAccessor;
    private readonly CreateEducationDto createEducationDto;
    private CreateEducationCommandHandler _handler { get; set; }
    public CreateEducationCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        }).CreateMapper();

        _mockPhotoAccessor = new Mock<IPhotoAccessor>();

         createEducationDto = new CreateEducationDto()
        {
            Id = Guid.NewGuid(),
            EducationInstitution = "Oxford University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            FieldOfStudy = "Oncology",
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            EducationInstitutionLogoId = "Oxford Campus",
        };

        _handler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper, _mockPhotoAccessor.Object);
    }

    [Fact]
    public async Task CreateEducationValid()
    {

        var photo = new PhotoUploadResult { PublicId = Guid.NewGuid().ToString(), Url = "photo-public-id" };
        _mockPhotoAccessor.Setup(pa => pa.AddPhoto(It.IsAny<IFormFile>())).ReturnsAsync(photo);

        createEducationDto.EducationInstitutionLogoId = photo.PublicId;
        var result = await _handler.Handle(new CreateEducationCommand() {createEducationDto = createEducationDto}, CancellationToken.None);
        Assert.IsType<CreateEducationDto>(result.Value);
        // Console.WriteLine("result", result);
        result.IsSuccess.ShouldBeTrue();
        result.Value.EducationInstitution.ShouldBeEquivalentTo(createEducationDto.EducationInstitution);
        result.Value.Degree.ShouldBeEquivalentTo(createEducationDto.Degree);
        result.Value.GraduationYear.ShouldBeEquivalentTo(createEducationDto.GraduationYear);
        result.Value.StartYear.ShouldBeEquivalentTo(createEducationDto.StartYear);

        var repo = await _mockUnitOfWork.Object.EducationRepository.GetAll();
        repo.Count.ShouldBe(3);
    }

    [Fact]
    public async Task CreateEducationInvalid()
    {
    
        CreateEducationDto createInvalid = new CreateEducationDto()
        {
            Id = Guid.NewGuid(),
            EducationInstitution = null,
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
        };

        var result = await _handler.Handle(new CreateEducationCommand() { createEducationDto = createInvalid }, CancellationToken.None);

        result.Value.ShouldBe(null);
    }
}

