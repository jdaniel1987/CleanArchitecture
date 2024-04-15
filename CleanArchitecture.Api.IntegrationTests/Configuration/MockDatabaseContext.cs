using CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.API.IntegrationTests.Configuration;

public static class MockDatabaseContext
{
    public static void ReplaceDatabaseContext(IServiceCollection services)
    {
        var context = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(DatabaseContext))!;
        services.Remove(context);
        var options = services.Where(r => (r.ServiceType == typeof(DbContextOptions))
          || (r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))).ToArray();
        foreach (var option in options)
        {
            services.Remove(option);
        }

        /* The options lambda is executed every time a request is made for an AppDbContext, instead of just once on app startup.
         * DON'T use Guid.NewGuid().ToString() inside UseInMemoryDatabase(dbName) method!! */
        var dbName = Guid.NewGuid().ToString();
        services.AddDbContextFactory<DatabaseContext>(options =>
            options.UseInMemoryDatabase(dbName));
    }
}
