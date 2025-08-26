using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RevisionWeek.Tests.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<API.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Optional: Configure services specifically for testing,
        // e.g., replace database contexts with in-memory versions.
        builder.ConfigureServices(services =>
        {
            // Example: Replace a DbContext with an in-memory version
            // services.RemoveAll<DbContextOptions<YourDbContext>>();
            // services.AddDbContext<YourDbContext>(options =>
            // {
            //     options.UseInMemoryDatabase("TestDatabase");
            // });
        });
    }
}