using CleanArchitecture.Services.Queries.GetGamesForConsole;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class GetGamesForConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_for_Console(
        IFixture fixture)
    {
        // Arrange
        var gamesConsoleIdToFind = fixture.Create<int>();
        var otherGames = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.GamesConsole, fixture
                .Build<Infrastructure.Models.GamesConsole>()
                .Without(c => c.Games)
                .Create())
            .CreateMany();
        var expectedGames = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.GamesConsoleId, gamesConsoleIdToFind)
            .With(g => g.GamesConsole, fixture
                .Build<Infrastructure.Models.GamesConsole>()
                .With(c => c.Id, gamesConsoleIdToFind)
                .Without(c => c.Games)
                .Create())
            .CreateMany();
        var existingGames = otherGames.Concat(expectedGames);

        var existingGamesConsoles = existingGames.Select(g => g.GamesConsole).Distinct().ToArray();
        await DbContext.GamesConsoles.AddRangeAsync(existingGamesConsoles);
        await DbContext.Games.AddRangeAsync(existingGames);
        await DbContext.SaveChangesAsync();

        var games = Mapper.Map<IReadOnlyCollection<Domain.Entities.Game>>(expectedGames);
        var expected = new GetGamesForConsoleResponse(games);

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesForConsoleResponse>($"api/GamesForConsole/{gamesConsoleIdToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
