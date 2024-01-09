using AutoFixture;
using AutoFixture.Xunit2;
using CleanArchitecture.Infrastructure.EmailSender;
using CleanArchitecture.Services.Commands.AddGame;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGameIntegrationTests : BaseIntegrationTests
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
        await _databaseContext.GamesConsoles.AddAsync(existingGamesConsole);
        await _databaseContext.SaveChangesAsync();

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/AddGame", addGameCommand);

        // Assert
        var actual = await _databaseContext.Games.SingleAsync(g => g.GamesConsoleId == existingGamesConsole.Id);
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
        _mockEmailLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", "random@email.com"), Times.Once);
    }
}
