using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;

    public GameRepository(
        DatabaseContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GameWithConsole>> GetAllGames()
    {
        var gameModels = await _dbContext.Games
            .Include(g => g.GamesConsole)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GameWithConsole>>(gameModels);
    }

    public async Task<IReadOnlyCollection<Game>> GetAllGamesForConsole(int gamesConsoleId)
    {
        var gameConsole = await _dbContext.GamesConsoles.FirstOrDefaultAsync(c => c.Id == gamesConsoleId) 
            ?? throw new Exception("Games console does not exist.");

        var gameModels = await _dbContext.Games
            .Where(x => x.GamesConsoleId == gamesConsoleId)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<Game>>(gameModels);
    }

    public async Task<Game> GetGame(int gameId)
    {
        var gameModel = await _dbContext.Games
            .FirstOrDefaultAsync(x => x.Id == gameId);

        return gameModel is null ? 
            throw new Exception("Game does not exist.") : 
            _mapper.Map<Game>(gameModel);
    }

    public async Task<IReadOnlyCollection<GameWithConsole>> GetGamesByName(string gameName)
    {
        var gameModels = await _dbContext.Games
            .Where(g => g.Name.Contains(gameName, StringComparison.OrdinalIgnoreCase))
            .Include(g => g.GamesConsole)
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GameWithConsole>>(gameModels);
    }

    public async Task AddGameToConsole(int gamesConsoleId, Game game)
    {
        var console = await _dbContext.GamesConsoles.SingleOrDefaultAsync(c => c.Id == gamesConsoleId)
            ?? throw new Exception("Console does not exist.");

        var gameModel = _mapper.Map<Models.Game>((gamesConsoleId, game));
        await _dbContext.Games.AddAsync(gameModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGame(Game game)
    {
        var gameModel = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id) 
            ?? throw new Exception("Game does not exist.");

        _dbContext.Games.Remove(gameModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateGame(Game game, int gamesConsoleId)
    {
        var existingGameModel = await _dbContext.Games.FirstOrDefaultAsync(c => c.Id == game.Id)
            ?? throw new Exception("Game does not exist.");

        var updatedGame = _mapper.Map<Models.Game>((gamesConsoleId, game));
        
        existingGameModel.Name = updatedGame.Name;
        existingGameModel.Publisher = updatedGame.Publisher;
        existingGameModel.GamesConsoleId = updatedGame.GamesConsoleId;
        existingGameModel.Price = updatedGame.Price;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGame(int gameId)
    {
        var gameToDelete = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId) 
            ?? throw new Exception("Game does not exist.");

        _dbContext.Games.Remove(gameToDelete);
        await _dbContext.SaveChangesAsync();
    }
}
