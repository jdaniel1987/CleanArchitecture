using Carter;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Services;

namespace CleanArchitecture.API;

public static class ServiceCollectionExtensions
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();

        services.AddDomain();
        services.AddInfrastructure();
        services.AddServices();
    }
}
