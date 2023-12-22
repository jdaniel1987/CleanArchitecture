using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Queries.GetAllGamesConsoles;

namespace CleanArchitecture.Services.Tests.Queries.GetAllGamesConsoles;

public class GetAllGamesConsolesHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_return_all_games_consoles(
        Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        GetAllGamesConsolesQuery getAllGamesConsolesQuery,
        IReadOnlyCollection<GamesConsole> mappedGamesConsoles)
    {
        var handler = new GetAllGamesConsolesHandler(
            gamesConsoleRepositoryMock.Object
        );
        gamesConsoleRepositoryMock.Setup(x => x.GetAllGamesConsoles()).ReturnsAsync(mappedGamesConsoles);

        var result = await handler.Handle(getAllGamesConsolesQuery, CancellationToken.None);

        gamesConsoleRepositoryMock.Verify(x => x.GetAllGamesConsoles(), Times.Once);
        result.Should().BeEquivalentTo(new GetAllGamesConsolesResponse(mappedGamesConsoles));
    }
}
