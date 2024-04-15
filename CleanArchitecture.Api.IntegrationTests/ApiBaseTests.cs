using CleanArchitecture.API.IntegrationTests.Configuration;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.EmailSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

public abstract class ApiBaseTests
{
    protected ApiBaseTests()
    {
        var fixture = new Fixture();
        MockEmailLogger = fixture.Freeze<Mock<ILogger<FakeEmailSender>>>();
        var app = new CustomWebApplicationFactory(services =>
        {
            services
                    .Replace(ServiceDescriptor.Transient(_ => MockEmailLogger.Object));
        });
        var scopeProvider = app.Services.GetService<IServiceScopeFactory>()!.CreateScope().ServiceProvider!;
        ApiClient = app.CreateClient();
        DbContext = scopeProvider.GetService<DatabaseContext>()!;
        //DatabaseSeed.SeedData(DbContext);
    }

    public HttpClient ApiClient { get; init; }

    public DatabaseContext DbContext { get; init; }

    public Mock<ILogger<FakeEmailSender>> MockEmailLogger  { get; init; }
}
