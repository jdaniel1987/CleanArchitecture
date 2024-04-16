namespace CleanArchitecture.API.IntegrationTests.Commands;

public class DeleteGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_delete_games_console(
        IFixture fixture,
        int gamesConsoleIdToDelete)
    {
        // Arrange
        var existingGamesConsole = fixture.Build<Infrastructure.Models.GamesConsole>()
            .With(c => c.Id, gamesConsoleIdToDelete)
            .Without(c => c.Games)
            .Create();
        await DbContext.GamesConsoles.AddAsync(existingGamesConsole);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.DeleteAsync($"api/DeleteGamesConsole/{gamesConsoleIdToDelete}");

        // Assert
        var actual = await DbContext.GamesConsoles.Where(g => g.Id == gamesConsoleIdToDelete).ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().BeEmpty();
    }
}
