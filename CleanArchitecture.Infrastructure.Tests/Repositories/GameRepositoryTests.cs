using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Tests.Repositories;
using GameDomain = CleanArchitecture.Domain.Entities.Game;
using GameModel = CleanArchitecture.Infrastructure.Models.Game;
using GamesConsoleDomain = CleanArchitecture.Domain.Entities.GamesConsole;
using GamesConsoleModel = CleanArchitecture.Infrastructure.Models.GamesConsole;

namespace DomainEventsExample.Infrastructure.Tests.Repositories;

public class GameRepositoryTests : RepositoryTestsBase<GameRepository>
{
    public static async Task<(IReadOnlyCollection<GamesConsoleModel> GamesConsoles, IReadOnlyCollection<GameModel> Games)>
        CreateExistingGames(
        IFixture fixture,
        DatabaseContext dbContext)
    {
        var existingGamesConsoles = fixture.Build<GamesConsoleModel>()
            .Without(c => c.Games)
            .CreateMany()
            .ToArray();
        var existingGames = existingGamesConsoles.SelectMany(c =>
            fixture.Build<GameModel>()
                .With(g => g.GamesConsoleId, c.Id)
                .Without(g => g.GamesConsole)
                .CreateMany())
            .ToArray();

        await dbContext.GamesConsoles.AddRangeAsync(existingGamesConsoles);
        await dbContext.Games.AddRangeAsync(existingGames);
        await dbContext.SaveChangesAsync();

        return (existingGamesConsoles, existingGames);
    }

    [Fact, AutoData]
    public async Task Should_get_games()
    {
        var (existingGamesConsoles, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);

        var actual = await RepositoryUnderTesting.GetAllGames();

        var gamesConsoleById = existingGamesConsoles.ToImmutableDictionary(c => c.Id);
        var expected = existingGames.Select(g =>
            new GameWithConsole(
                Mapper.Map<GameDomain>(g),
                Mapper.Map<GamesConsoleDomain>(gamesConsoleById[g.GamesConsoleId])));
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact, AutoData]
    public async Task Should_get_games_for_console()
    {
        var (_, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);

        var gamesConsoleToFind = existingGames.First();
        var actual = await RepositoryUnderTesting.GetAllGamesForConsole(gamesConsoleToFind.GamesConsoleId);

        var expectedModels = existingGames.Where(g => g.GamesConsoleId == gamesConsoleToFind.GamesConsoleId);
        var expected = Mapper.Map<IReadOnlyCollection<GameDomain>>(expectedModels);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact, AutoData]
    public async Task Should_get_game()
    {
        var (_, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);

        var expectedModel = existingGames.First();
        var actual = await RepositoryUnderTesting.GetGame(expectedModel.Id);

        var expected = Mapper.Map<GameDomain>(expectedModel);
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Should_get_games_by_name()
    {
        var (_, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);
        var nameToFind = Fixture.Create<string>();
        var game1ToFind = Fixture.Build<GameModel>()
            .With(g => g.GamesConsoleId, existingGames.First().GamesConsoleId)
            .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
            .Without(g => g.GamesConsole)
            .Create();
        var game2ToFind = Fixture.Build<GameModel>()
            .With(g => g.GamesConsoleId, existingGames.First().GamesConsoleId)
            .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
            .Without(g => g.GamesConsole)
            .Create();
        await DatabaseContext.Games.AddAsync(game1ToFind);
        await DatabaseContext.Games.AddAsync(game2ToFind);
        await DatabaseContext.SaveChangesAsync();

        var actual = await RepositoryUnderTesting.GetGamesByName(nameToFind);

        var expected = Mapper.Map<IReadOnlyCollection<GameWithConsole>>(new[] { game1ToFind, game2ToFind });
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Should_add_game_to_console(
        GameDomain newGameDomain)
    {
        var existingGamesConsoleModel = Fixture.Build<GamesConsoleModel>()
            .Without(c => c.Games)
            .Create();
        await DatabaseContext.GamesConsoles.AddAsync(existingGamesConsoleModel);
        await DatabaseContext.SaveChangesAsync();

        await RepositoryUnderTesting.AddGameToConsole(existingGamesConsoleModel.Id, newGameDomain);

        var expected = Mapper.Map<GameModel>((existingGamesConsoleModel.Id, newGameDomain));
        var actual = await DatabaseContext.Games.SingleAsync();
        actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
    }

    [Theory, AutoData]
    public async Task Should_update_game(
        GameDomain updatedGame)
    {
        var (_, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);

        var existingGameToUpdate = existingGames.First();
        var updatedGameFixed = updatedGame with { Id = existingGameToUpdate.Id };
        await RepositoryUnderTesting.UpdateGame(updatedGameFixed, existingGameToUpdate.GamesConsoleId);

        var expected = Mapper.Map<GameModel>((existingGameToUpdate.GamesConsoleId, updatedGameFixed));
        var actual = await DatabaseContext.Games.FirstAsync();
        actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
    }

    [Theory, AutoData]
    public async Task Should_delete_game()
    {
        var (_, existingGames) = await CreateExistingGames(Fixture, DatabaseContext);

        var gameToDelete = existingGames.First();
        await RepositoryUnderTesting.DeleteGame(gameToDelete.Id);

        var actual = await DatabaseContext.Games.Where(g => g.Id == gameToDelete.Id).ToArrayAsync();
        actual.Should().BeEmpty();
    }
}
