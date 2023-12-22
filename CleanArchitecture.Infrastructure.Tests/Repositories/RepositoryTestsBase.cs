using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Mappers;

namespace CleanArchitecture.Infrastructure.Tests.Repositories;

public class RepositoryTestsBase<T>
{
    protected DatabaseContext DatabaseContext { get; private set; }
    protected T RepositoryUnderTesting { get; private set; }
    protected IMapper Mapper { get; private set; }

    public RepositoryTestsBase()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        DatabaseContext = new DatabaseContext(options);

        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GamesConsoleProfile());
            mc.AddProfile(new GameProfile());
            mc.AddProfile(new GameWithConsoleProfile());
        });

        Mapper = new Mapper(mappingConfig);
        fixture.Register(() => Mapper);

        RepositoryUnderTesting = (T)Activator.CreateInstance(typeof(T), new object[] { DatabaseContext, Mapper })!;
    }

    public void Dispose()
    {
        // Clean resources after each test
        DatabaseContext.Dispose();
    }
}
