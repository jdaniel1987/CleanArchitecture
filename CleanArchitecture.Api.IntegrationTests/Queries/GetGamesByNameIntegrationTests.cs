using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Services.Queries.GetGamesByName;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class GetGamesByNameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_by_name(
        IFixture fixture)
    {
        // Arrange
        var nameToFind = fixture.Create<string>();
        var otherGames = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.GamesConsole, fixture
                .Build<Infrastructure.Models.GamesConsole>()
                .Without(c => c.Games)
                .Create())
            .CreateMany();
        var expectedGames = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.Name, $"{fixture.Create<string>()}{nameToFind}{fixture.Create<string>()}")
            .With(g => g.GamesConsole, fixture
                .Build<Infrastructure.Models.GamesConsole>()
                .Without(c => c.Games)
                .Create())
            .CreateMany();
        var existingGames = otherGames.Concat(expectedGames);

        var existingGamesConsoles = existingGames.Select(g => g.GamesConsole).Distinct().ToArray();
        await DbContext.GamesConsoles.AddRangeAsync(existingGamesConsoles);
        await DbContext.Games.AddRangeAsync(existingGames);
        await DbContext.SaveChangesAsync();

        var gamesWithConsoles = Mapper.Map<IReadOnlyCollection<GameWithConsole>>(expectedGames);
        var expected = new GetGamesByNameResponse(gamesWithConsoles);

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesByNameResponse>($"api/GamesByName/{nameToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
