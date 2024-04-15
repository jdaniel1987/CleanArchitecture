using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.API.IntegrationTests.Configuration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _overrideDependencies;

    public CustomWebApplicationFactory(Action<IServiceCollection>? overrideDependencies = null)
    {
        _overrideDependencies = overrideDependencies;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        builder.ConfigureTestServices(services =>
        {
            var fixture = new Fixture();

            _overrideDependencies?.Invoke(services);

            MockDatabaseContext.ReplaceDatabaseContext(services);
        });
    }
}
