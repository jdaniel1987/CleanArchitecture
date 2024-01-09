using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Infrastructure.Tests.Repositories;

public sealed class DatabaseContextCustomization : ICustomization
{
    // Give every fixture using this customization their own in-memory database
    private readonly string _databaseName = Guid.NewGuid().ToString();

    public void Customize(IFixture fixture)
    {
        fixture.Register(() =>
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(_databaseName)
                .Options;

            return new DatabaseContext(options);
        });

        fixture.Register(() =>
        {
            var databaseContextFactory = fixture.Create<Mock<IDbContextFactory<DatabaseContext>>>();
            databaseContextFactory
                .Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(fixture.Create<DatabaseContext>);

            return databaseContextFactory.Object;
        });
    }
}
