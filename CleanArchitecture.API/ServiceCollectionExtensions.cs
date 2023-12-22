using Carter;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Services;

namespace CleanArchitecture.API;

public static class ServiceCollectionExtensions
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDomain();
        services.AddInfrastructure();
        services.AddServices();
    }
}
