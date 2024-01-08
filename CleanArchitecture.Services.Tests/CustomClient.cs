using AutoFixture;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CleanArchitecture.Services.Tests;

public static class CustomClient
{
    public static HttpClient CreateCustomClient(IFixture fixture)
    {
        var mockMediator = fixture.Freeze<Mock<IMediator>>();

        var application = new CustomWebApplicationFactory(services =>
        {
            services
                .Replace(ServiceDescriptor.Transient(_ => mockMediator.Object));
        });

        return application.CreateClient();
    }
}
