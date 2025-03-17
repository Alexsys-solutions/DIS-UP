namespace Men.ProfessorAssignmentApi.Tests.Bdd.Feature;

[Binding]
internal class FeatureSteps : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public FeatureSteps(TestWebApplicationFactory factory)
    {
        _factory = factory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => {
#pragma warning disable S125
                    // Add your SPECIFIC mocks here
                    // By example mocked repositories with data coming from Feature table
#pragma warning restore S125
                }));

        _client = _factory.CreateClient();
    }
}
