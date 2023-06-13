namespace Application.UnitTest.EducationTest.EducationQueryTest;

public class GetEducationDetailCommandHandlerTest
{

    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
    private GetEducationDetailsQueryHandler _handler { get; set; }

    public GetBlogDetailsQueryHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        }).CreateMapper();

        _handler = new GetEducationDetailsQueryHandler(_mockUnitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task GetEducationDetailsValid()
    {
        var result = await _handler.Handle(new GetEducationDetailsQuery() { Id = 1 }, CancellationToken.None);
        result.ShouldNotBe(null);
    }

    [Fact]
    public async Task GetBlogDetailsInvalid()
    {
        var result = await _handler.Handle(new GetEducationDetailsQuery() { Id = 100 }, CancellationToken.None);
        result.Value.ShouldBe(null);
    }
}


