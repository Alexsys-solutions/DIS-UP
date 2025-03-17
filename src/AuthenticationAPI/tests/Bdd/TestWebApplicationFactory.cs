namespace Men.ProfessorAssignmentApi.Tests.Bdd;

internal class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Production")
            .UseContentRoot(".")
            .ConfigureTestServices(
                services => {
#pragma warning disable S125
                    // Add your GLOBAL mocks here
                    /* Exemple to mock HttpClient :
                        services
                        .AddHttpClient([MyHttpClient])
                        .AddHttpMessageHandler(() => new GlobalServiceHandler());
                    */
#pragma warning restore S125
                });

        base.ConfigureWebHost(builder);
    }
}
