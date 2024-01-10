using CleanArchitecture.Services.Commands.AddGamesConsole;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGamesConsoleIntegrationTests : BaseIntegrationTests
{
    [Theory, AutoData]
    public async Task Should_add_game_console(
        AddGamesConsoleCommand addGamesConsoleCommand)
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync("api/AddGamesConsole", addGamesConsoleCommand);

        // Assert
        var actual = await _databaseContext.GamesConsoles.SingleAsync();
        var expected = new Infrastructure.Models.GamesConsole()
        {
            Manufacturer = addGamesConsoleCommand.Manufacturer,
            Name = addGamesConsoleCommand.Name,
            Price = addGamesConsoleCommand.Price,
        };

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Should().BeEquivalentTo(expected, 
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.Games));
    }
}
