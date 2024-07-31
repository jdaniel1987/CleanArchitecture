using CleanArchitecture.Services.Commands.AddGamesConsole;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_games_console(
        AddGamesConsoleCommand addGamesConsoleCommand)
    {
        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGamesConsole", addGamesConsoleCommand);

        var expected = new Infrastructure.Models.GamesConsole()
        {
            Id = 1,
            Manufacturer = addGamesConsoleCommand.Manufacturer,
            Name = addGamesConsoleCommand.Name,
            Price = addGamesConsoleCommand.Price,
        };


        // Assert
        var actual = await DbContext.GamesConsoles.SingleAsync(c => c.Name == addGamesConsoleCommand.Name);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Should().BeEquivalentTo(expected, 
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.Games));
    }
}
