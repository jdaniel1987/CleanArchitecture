using CleanArchitecture.Services.Commands.AddGame;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_game(
        IFixture fixture,
        AddGameCommand addGameCommand)
    {
        // Arrange
        var existingGamesConsole = fixture.Build<Infrastructure.Models.GamesConsole>()
            .With(c => c.Id, addGameCommand.GamesConsoleId)
            .Without(c => c.Games)
            .Create();
        await DbContext.GamesConsoles.AddAsync(existingGamesConsole);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGame", addGameCommand);

        // Assert
        var actual = await DbContext.Games.SingleAsync(g => g.GamesConsoleId == existingGamesConsole.Id);
        var expected = new Infrastructure.Models.Game()
        {
            Publisher = addGameCommand.Publisher,
            GamesConsoleId = addGameCommand.GamesConsoleId,
            Name = addGameCommand.Name,
            Price = addGameCommand.Price,
        };

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Should().BeEquivalentTo(expected, 
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GamesConsole));
        MockEmailLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", "random@email.com"), Times.Once);
    }
}
