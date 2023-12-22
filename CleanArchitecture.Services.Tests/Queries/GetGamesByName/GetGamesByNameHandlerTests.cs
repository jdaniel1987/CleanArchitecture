using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Queries.GetGamesByName;

namespace CleanArchitecture.Services.Tests.Queries.GetGamesByName;

public class GetGamesByNameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_return_games_by_name(
        IFixture fixture,
        Mock<IGameRepository> gameRepositoryMock,
        IReadOnlyCollection<GameWithConsole> mappedGames)
    {
        var handler = new GetGamesByNameHandler(
            gameRepositoryMock.Object
        );
        var nameToFind = fixture.Create<string>();
        var getGamesByNameQuery = fixture.Build<GetGamesByNameQuery>()
            .With(q => q.GameName, nameToFind)
            .Create();
        gameRepositoryMock.Setup(x => x.GetGamesByName(nameToFind)).ReturnsAsync(mappedGames);

        var result = await handler.Handle(getGamesByNameQuery, CancellationToken.None);

        gameRepositoryMock.Verify(x => x.GetGamesByName(nameToFind), Times.Once);
        result.Should().BeEquivalentTo(new GetGamesByNameResponse(mappedGames));
    }
}
