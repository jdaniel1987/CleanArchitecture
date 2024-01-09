using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Mappers;
using Moq;

namespace CleanArchitecture.Infrastructure.Tests.Repositories;

public class RepositoryTestsBase<T> : IDisposable
{
    protected DatabaseContext DatabaseContext { get; private set; }
    protected T RepositoryUnderTesting { get; private set; }
    protected IMapper Mapper { get; private set; }
    protected IFixture Fixture { get; private set; }

    public RepositoryTestsBase()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        DatabaseContext = new DatabaseContext(options);
        DatabaseContext.Database.EnsureCreated(); // Ensure database is created

        var dbContextFactoryMock = new Mock<IDbContextFactory<DatabaseContext>>();
        dbContextFactoryMock.Setup(factory => factory.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(DatabaseContext);

        Fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GamesConsoleProfile());
            mc.AddProfile(new GameProfile());
            mc.AddProfile(new GameWithConsoleProfile());
        });

        Mapper = new Mapper(mappingConfig);
        Fixture.Register(() => Mapper);
        Fixture.Register(() => DatabaseContext);

        RepositoryUnderTesting = (T)Activator.CreateInstance(typeof(T), new object[] { dbContextFactoryMock.Object, Mapper })!;
    }

    public void Dispose()
    {
        DatabaseContext.Dispose();
    }
}
