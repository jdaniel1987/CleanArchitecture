using AutoFixture.Xunit2;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Queries.GetAllGames;
using FluentAssertions;
using Moq;

namespace CleanArchitecture.Services.Tests.Queries.GetAllGames;

public class GetAllGamesHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_return_all_games(
        Mock<IGameRepository> gameRepositoryMock,
        GetAllGamesQuery getAllGamesQuery,
        IReadOnlyCollection<GameWithConsole> mappedGames)
    {
        var handler = new GetAllGamesHandler(
            gameRepositoryMock.Object
        );
        gameRepositoryMock.Setup(x => x.GetAllGames()).ReturnsAsync(mappedGames);

        var result = await handler.Handle(getAllGamesQuery, CancellationToken.None);

        gameRepositoryMock.Verify(x => x.GetAllGames(), Times.Once);
        result.Should().BeEquivalentTo(new GetAllGamesResponse(mappedGames));
    }
}
