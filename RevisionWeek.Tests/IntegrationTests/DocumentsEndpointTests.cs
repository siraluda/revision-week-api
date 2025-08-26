namespace RevisionWeek.Tests.IntegrationTests;

public class DocumentsEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DocumentsEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
}