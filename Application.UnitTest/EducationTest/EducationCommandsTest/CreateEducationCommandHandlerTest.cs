namespace Application.UnitTest.EducationTest.EducationCommandsTest;

public class CreateEducationCommandHandlerTest
{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private CreateEducationCommandHandler _handler { get; set; }
    public CreateEducationCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        }).CreateMapper();

        _handler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task CreateEducationValid()
    {

        CreateEducationDto createEducationDto = new()
        {
        };

        var result = await _handler.Handle(new CreateEducationCommand() { CreateEducationDto = createEducationDto }, CancellationToken.None);

        result.Value.Content.ShouldBeEquivalentTo(createEducationDto.Educatio);
        result.Value.Title.ShouldBeEquivalentTo(createBlogDto.Title);

        (await _mockUnitOfWork.Object.BlogRepository.GetAll()).Count.ShouldBe(3);
    }

    [Fact]
    public async Task CreateBlogInvalid()
    {

        CreateBlogDto createBlogDto = new()
        {
            Title = "", // Title can't be empty 
            Content = "Body of the new blog",
            Publish = true,
        };

        var result = await _handler.Handle(new CreateBlogCommand() { CreateBlogDto = createBlogDto }, CancellationToken.None);

        result.Value.ShouldBe(null);
    }
}

