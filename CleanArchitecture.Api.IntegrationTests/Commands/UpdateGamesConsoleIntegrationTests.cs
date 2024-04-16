using CleanArchitecture.Services.Commands.UpdateGamesConsole;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class UpdateGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_games_console(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsole = fixture
            .Build<Infrastructure.Models.GamesConsole>()
            .Without(c => c.Games)
            .Create();
        await DbContext.GamesConsoles.AddAsync(existingGamesConsole);
        await DbContext.SaveChangesAsync();

        var updateGamesConsoleCommand = fixture
            .Build<UpdateGamesConsoleCommand>()
            .With(c => c.Id, existingGamesConsole.Id)
            .Create();
        var gamesConsoleUpdatedDomain = Mapper.Map<Domain.Entities.GamesConsole>(updateGamesConsoleCommand);
        var gamesConsoleUpdatedModel = Mapper.Map<Infrastructure.Models.GamesConsole>(gamesConsoleUpdatedDomain);

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGamesConsole", updateGamesConsoleCommand);

        // Assert
        var actual = await DbContext
            .GamesConsoles
            .AsNoTracking()
            .Where(g => g.Id == updateGamesConsoleCommand.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().BeEquivalentTo(gamesConsoleUpdatedModel);
    }
}
