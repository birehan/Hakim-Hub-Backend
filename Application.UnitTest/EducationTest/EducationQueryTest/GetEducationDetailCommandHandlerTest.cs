using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS.Handlers;
using Application.Features.Educations.CQRS.Queries;
using Application.Features.Educations.DTOs;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Moq;
using Xunit;
using Shouldly;
using Application.Responses;

namespace Application.UnitTest.EducationTest.EducationQueryTest;

public class GetEducationDetailCommandHandlerTest
{
    private Mock<IMapper> _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private GetEducationByIdQueryHandler _handler { get; set; }

    public GetEducationDetailCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        _mapper = new Mock<IMapper>();
        _handler = new GetEducationByIdQueryHandler(_mockUnitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetEducationDetailsValid()
    {
        var educationId = Guid.NewGuid();
        Education education = new(){
            Id = educationId,
            EducationInstitution = "Belford University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            EducationInstitutionLogoId = "Belford Campus"
        };
        EducationDto educationDto = new()
        {
            Id = educationId,
            EducationInstitution = "Belford University",
            StartYear = DateTime.Now,
            GraduationYear = DateTime.Today,
            Degree = "Bachelors",
            DoctorId = Guid.NewGuid(),
            EducationInstitutionLogoId = "Belford Campus"
        };

        _mockUnitOfWork.Setup(uow => uow.EducationRepository.Get(educationId)).ReturnsAsync(education);
        _mapper.Setup(mapper => mapper.Map<EducationDto>(education)).Returns(educationDto);
        var result = await _handler.Handle(new GetEducationDetailQuery() { Id = educationDto.Id }, CancellationToken.None);
        result.ShouldNotBe(null);
        Assert.IsType<Result<EducationDto>>(result);
        Assert.IsType<EducationDto>(result.Value);
        Assert.Equal(educationId, result.Value.Id);

    }

    [Fact]
    public async Task GetEducationDetailsInvalid()
    {
        var result = await _handler.Handle(new GetEducationDetailQuery() { Id = Guid.NewGuid() }, CancellationToken.None);
        result.Value.ShouldBe(null);
        Assert.Equal("Fetch Failed", result.Error);
    }
}


