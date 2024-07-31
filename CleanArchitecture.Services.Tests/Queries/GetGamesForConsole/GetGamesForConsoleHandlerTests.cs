using AutoFixture;
using AutoFixture.Xunit2;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Queries.GetGamesForConsole;
using FluentAssertions;
using Moq;

namespace CleanArchitecture.Services.Tests.Queries.GetGamesByName;

public class GetGamesForConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_throw_error_if_games_console_does_not_exist(
    IFixture fixture,
    Mock<IGameRepository> gameRepositoryMock,
    Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
    IReadOnlyCollection<Game> mappedGames)
    {
        var handler = new GetGamesForConsoleHandler(
            gameRepositoryMock.Object,
            gamesConsoleRepositoryMock.Object
        );
        var gamesConsoleToFind = fixture.Create<int>();
        var getGamesForConsoleQuery = fixture.Build<GetGamesForConsoleQuery>()
            .With(q => q.GamesConsoleId, gamesConsoleToFind)
            .Create();
        gameRepositoryMock.Setup(x => x.GetAllGamesForConsole(gamesConsoleToFind)).ReturnsAsync(mappedGames);

        await handler.Invoking(h => h.Handle(getGamesForConsoleQuery, CancellationToken.None)).Should().ThrowAsync<Exception>();

        gameRepositoryMock.Verify(x => x.GetAllGamesForConsole(gamesConsoleToFind), Times.Never);
    }

    [Theory, AutoData]
    public async Task Handle_should_return_games_for_console(
        IFixture fixture,
        Mock<IGameRepository> gameRepositoryMock,
        Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        GamesConsole mapperGamesConsole,
        IReadOnlyCollection<Game> mappedGames)
    {
        var handler = new GetGamesForConsoleHandler(
            gameRepositoryMock.Object,
            gamesConsoleRepositoryMock.Object
        );
        var gamesConsoleToFind = fixture.Create<int>();
        var getGamesForConsoleQuery = fixture.Build<GetGamesForConsoleQuery>()
            .With(q => q.GamesConsoleId, gamesConsoleToFind)
            .Create();
        gamesConsoleRepositoryMock.Setup(x => x.GetGamesConsole(gamesConsoleToFind)).ReturnsAsync(mapperGamesConsole);
        gameRepositoryMock.Setup(x => x.GetAllGamesForConsole(gamesConsoleToFind)).ReturnsAsync(mappedGames);

        var result = await handler.Handle(getGamesForConsoleQuery, CancellationToken.None);

        gameRepositoryMock.Verify(x => x.GetAllGamesForConsole(gamesConsoleToFind), Times.Once);
        result.Should().BeEquivalentTo(new GetGamesForConsoleResponse(mappedGames));
    }
}
