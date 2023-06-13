namespace Application.UnitTest.EducationTest.EducationQueryTest;

public class GetEducationListCommandHandlerTest
{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private GetEducationListQueryHandler _handler { get; set; }


    public GetChoreListQueriesHandlersTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new GetEducationListQueryHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task GetCEducationListValid()
    {
        var result = await _handler.Handle(new GetEducationListQuery(), CancellationToken.None);
        result.Value.Count.ShouldNotBe(1);
    }

}
