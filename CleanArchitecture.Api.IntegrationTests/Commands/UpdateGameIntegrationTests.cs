using CleanArchitecture.Services.Commands.UpdateGame;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class UpdateGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_game(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsole = fixture
            .Build<Infrastructure.Models.GamesConsole>()
            .Without(c => c.Games)
            .Create();
        var existingGame = fixture
            .Build<Infrastructure.Models.Game>()
            .With(g => g.GamesConsoleId, existingGamesConsole.Id)
            .Without(c => c.GamesConsole)
            .Create();
        await DbContext.GamesConsoles.AddAsync(existingGamesConsole);
        await DbContext.Games.AddAsync(existingGame);
        await DbContext.SaveChangesAsync();

        var updateGameCommand = fixture
            .Build<UpdateGameCommand>()
            .With(c => c.Id, existingGame.Id)
            .Create();

        var expected = new Infrastructure.Models.Game()
        {
            Id = updateGameCommand.Id,
            Name = updateGameCommand.Name,
            GamesConsoleId = updateGameCommand.GamesConsoleId,
            Price = updateGameCommand.Price,
            Publisher = updateGameCommand.Publisher,
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGame", updateGameCommand);

        // Assert
        var actual = await DbContext
            .Games
            .AsNoTracking()
            .Where(g => g.Id == updateGameCommand.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().BeEquivalentTo(expected);
    }
}
