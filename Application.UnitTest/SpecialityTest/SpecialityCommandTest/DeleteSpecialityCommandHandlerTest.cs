

using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Commands;
using Application.Features.Specialities.CQRS.Handlers;
using Application.Features.Specialities.DTOs;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Moq;
using Shouldly;

namespace Application.UnitTest.SpecialityTest.SpecialityCommandTest;

public class DeleteSpecialityCommandHandlerTest
{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private DeleteSpecialityCommandHandler _handler { get; set; }
    private readonly CreateSpecialityDto createSpecialityDto;
    private CreateSpecialityCommandHandler _createHandler { get; set; }


    public DeleteSpecialityCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        // _mapper = new Mock<IMapper>();

        _mapper = new MapperConfiguration(c =>
      {
          c.AddProfile<MappingProfile>();
      }).CreateMapper();

        _handler = new DeleteSpecialityCommandHandler(_mockUnitOfWork.Object);

        createSpecialityDto = new CreateSpecialityDto()
        {
            Id = Guid.NewGuid(),
            Name = "Oncology",
            Description = "This is a sample description for oncology."
        };
        _createHandler = new CreateSpecialityCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task DeleteSpecialityValid()
    {
        var result = await _createHandler.Handle(new CreateSpecialityCommand() { SpecialityDto = createSpecialityDto }, CancellationToken.None);
        var educationToBeDeleted = result.Value.Id;

        var num = (await _mockUnitOfWork.Object.EducationRepository.GetAll()).Count;
        (await _mockUnitOfWork.Object.SpecialityRepository.GetAll()).Count.ShouldBe(3);
        var resultAfterDeletion = await _handler.Handle(new DeleteSpecialityCommand() { Id = educationToBeDeleted }, CancellationToken.None);
        resultAfterDeletion.Value.ShouldBeEquivalentTo(educationToBeDeleted);
        resultAfterDeletion.Error.ShouldBeEquivalentTo("Speciality Deleted Successfully.");
        (await _mockUnitOfWork.Object.SpecialityRepository.GetAll()).Count.ShouldBe(2);
        Assert.IsType<Result<Guid?>>(resultAfterDeletion);

    }


    [Fact]
    public async Task DeleteSpecialityInvalid()
    {
        var result = await _handler.Handle(new DeleteSpecialityCommand() { Id = Guid.NewGuid() }, CancellationToken.None);
        result.Value.ShouldBeEquivalentTo(null);
        result.Error.ShouldBeEquivalentTo("Speciality Not Found.");
        Assert.IsType<Result<Guid?>>(result);

    }
}
