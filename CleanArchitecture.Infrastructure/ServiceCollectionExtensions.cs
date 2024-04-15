using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArchitecture.Domain.EmailSender;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.EmailSender;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Infrastructure;

public static class ServiceCollectionExtensions
{    
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender, FakeEmailSender>();
        services.AddTransient<IGamesConsoleRepository, GamesConsoleRepository>();
        services.AddTransient<IGameRepository, GameRepository>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var dbName = nameof(DatabaseContext);
        services.AddDbContextFactory<DatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(dbName));
    }
}
