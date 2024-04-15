namespace CleanArchitecture.API.IntegrationTests.Commands;

public class DeleteGameIntegrationTests : ApiBaseTests
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
        await DbContext.Games.AddAsync(existingGame);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.DeleteAsync($"api/DeleteGame/{gameIdToDelete}");

        // Assert
        var actual = await DbContext.Games.Where(g => g.Id == gameIdToDelete).ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().BeEmpty();
    }
}
