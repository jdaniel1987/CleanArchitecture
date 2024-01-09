using AutoFixture;
using AutoMapper;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.EmailSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.API.IntegrationTests;

public class BaseIntegrationTests
{
    protected HttpClient _httpClient;
    protected DatabaseContext _databaseContext;
    protected IMapper _mapper;
    protected Mock<ILogger<FakeEmailSender>> _mockEmailLogger;

    public BaseIntegrationTests()
    {
        var fixture = new Fixture();
        _mockEmailLogger = fixture.Create<Mock<ILogger<FakeEmailSender>>>();
        var application = new CustomWebApplicationFactory(services =>
        {
            services
                    .Replace(ServiceDescriptor.Transient(_ => _mockEmailLogger.Object));
        });
        _httpClient = application.CreateClient();

        var dbContextFactory = application.Services.GetService<IDbContextFactory<DatabaseContext>>()!;
        _databaseContext = dbContextFactory.CreateDbContext();

        _mapper = application.Services.GetService<IMapper>()!;
    }
}
