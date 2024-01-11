namespace CleanArchitecture.API.IntegrationTests.Commands;

public class DeleteGameIntegrationTests : BaseIntegrationTests
{
    [Theory, AutoData]
    public async Task Should_delete_game(
        IFixture fixture,
        int gameIdToDelete)
    {
        // Arrange
        var existingGame = fixture.Build<Infrastructure.Models.Game>()
            .With(c => c.Id, gameIdToDelete)
            .Without(c => c.GamesConsole)
            .Create();
        await _databaseContext.Games.AddAsync(existingGame);
        await _databaseContext.SaveChangesAsync();

        // Act
        var response = await _httpClient.DeleteAsync($"api/DeleteGame/{gameIdToDelete}");

        // Assert
        var actual = await _databaseContext.Games.Where(g => g.Id == gameIdToDelete).ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().BeEmpty();
    }
}
