using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Infrastructure.Repositories;

public class GameRepository(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IMapper mapper) : IGameRepository
{
    private readonly IDbContextFactory<DatabaseContext> _databaseContextFactory = databaseContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<GameWithConsole>> GetAllGames()
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameModels = await databaseContext
            .Games
            .Include(g => g.GamesConsole)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GameWithConsole>>(gameModels);
    }

    public async Task<IReadOnlyCollection<Game>> GetAllGamesForConsole(int gamesConsoleId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameConsole = await databaseContext
            .GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsoleId) 
            ?? throw new Exception("Games console does not exist.");

        var gameModels = await databaseContext
            .Games
            .Where(x => x.GamesConsoleId == gamesConsoleId)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<Game>>(gameModels);
    }

    public async Task<Game> GetGame(int gameId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameModel = await databaseContext
            .Games
            .FirstOrDefaultAsync(x => x.Id == gameId);

        return gameModel is null ? 
            throw new Exception("Game does not exist.") : 
            _mapper.Map<Game>(gameModel);
    }

    public async Task<IReadOnlyCollection<GameWithConsole>> GetGamesByName(string gameName)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameModels = await databaseContext
            .Games
            .Where(g => g.Name.Contains(gameName, StringComparison.OrdinalIgnoreCase))
            .Include(g => g.GamesConsole)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GameWithConsole>>(gameModels);
    }

    public async Task AddGameToConsole(int gamesConsoleId, Game game)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var console = await databaseContext
            .GamesConsoles
            .SingleOrDefaultAsync(c => c.Id == gamesConsoleId)
            ?? throw new Exception("Console does not exist.");

        var gameModel = _mapper.Map<Models.Game>((gamesConsoleId, game));
        await databaseContext.Games.AddAsync(gameModel);
        await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteGame(Game game)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameModel = await databaseContext
            .Games
            .FirstOrDefaultAsync(g => g.Id == game.Id) 
            ?? throw new Exception("Game does not exist.");

        databaseContext.Games.Remove(gameModel);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateGame(Game game, int gamesConsoleId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var existingGameModel = await databaseContext
            .Games
            .FirstOrDefaultAsync(c => c.Id == game.Id)
            ?? throw new Exception("Game does not exist.");

        var updatedGame = _mapper.Map<Models.Game>((gamesConsoleId, game));
        
        existingGameModel.Name = updatedGame.Name;
        existingGameModel.Publisher = updatedGame.Publisher;
        existingGameModel.GamesConsoleId = updatedGame.GamesConsoleId;
        existingGameModel.Price = updatedGame.Price;
        await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteGame(int gameId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gameToDelete = await databaseContext
            .Games
            .FirstOrDefaultAsync(g => g.Id == gameId) 
            ?? throw new Exception("Game does not exist.");

        databaseContext.Games.Remove(gameToDelete);
        await databaseContext.SaveChangesAsync();
    }
}
