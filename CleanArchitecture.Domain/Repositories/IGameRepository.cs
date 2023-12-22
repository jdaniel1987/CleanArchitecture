using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Aggregates;

namespace CleanArchitecture.Domain.Repositories;

public interface IGameRepository
{
    Task<IReadOnlyCollection<GameWithConsole>> GetAllGames();
    Task<IReadOnlyCollection<Game>> GetAllGamesForConsole(int gamesConsoleId);
    Task<Game> GetGame(int gameId);
    Task<IReadOnlyCollection<GameWithConsole>> GetGamesByName(string gameName);
    Task AddGameToConsole(int gamesConsoleId, Game game);
    Task UpdateGame(Game game, int gamesConsoleId);
    Task DeleteGame(int gameId);
}
