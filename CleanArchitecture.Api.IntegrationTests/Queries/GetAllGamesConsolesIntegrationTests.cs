using CleanArchitecture.Services.Queries.GetAllGamesConsoles;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class GetAllGamesConsolesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games_consoles(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsoles = fixture
            .Build<Infrastructure.Models.GamesConsole>()
            .Without(g => g.Games)
            .CreateMany();

        await DbContext.GamesConsoles.AddRangeAsync(existingGamesConsoles);
        await DbContext.SaveChangesAsync();

        var gamesConsoles = Mapper.Map<IReadOnlyCollection<Domain.Entities.GamesConsole>>(existingGamesConsoles);
        var expected = new GetAllGamesConsolesResponse(gamesConsoles);

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGamesConsolesResponse>("api/GamesConsoles");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
