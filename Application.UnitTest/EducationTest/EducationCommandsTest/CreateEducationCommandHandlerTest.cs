using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS;
using Application.Features.Educations.CQRS.Handlers;
using Application.Features.Educations.DTOs;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Moq;
using Shouldly;

namespace Application.UnitTest.EducationTest.EducationCommandsTest;

public class CreateEducationCommandHandlerTest
{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private readonly CreateEducationDto createEducationDto;
    private CreateEducationCommandHandler _handler { get; set; }
    public CreateEducationCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        }).CreateMapper();

         createEducationDto = new CreateEducationDto()
        {
            Id = Guid.NewGuid(),
            EducationInstitution = "Oxford University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            FieldOfStudy = "Oncology",
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            InstitutionLogoId = "Oxford Campus"
        };

        _handler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task CreateEducationValid()
    {
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
        createEducationDto.FieldOfStudy = null;

        var result = await _handler.Handle(new CreateEducationCommand() { createEducationDto = createEducationDto }, CancellationToken.None);

        result.Value.ShouldBe(null);
    }
}

