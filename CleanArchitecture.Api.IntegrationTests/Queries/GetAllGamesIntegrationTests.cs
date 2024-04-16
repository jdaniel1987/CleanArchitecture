using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Services.Queries.GetAllGames;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class GetAllGamesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games(
        IFixture fixture)
    {
        // Arrange
        var existingGames = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.GamesConsole, fixture
                .Build<Infrastructure.Models.GamesConsole>()
                .Without(c => c.Games)
                .Create())
            .CreateMany();

        var existingGamesConsoles = existingGames.Select(g => g.GamesConsole).Distinct().ToArray();
        await DbContext.GamesConsoles.AddRangeAsync(existingGamesConsoles);
        await DbContext.Games.AddRangeAsync(existingGames);
        await DbContext.SaveChangesAsync();

        var gamesWithConsoles = Mapper.Map<IReadOnlyCollection<GameWithConsole>>(existingGames);
        var expected = new GetAllGamesResponse(gamesWithConsoles);

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGamesResponse>("api/Games");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
