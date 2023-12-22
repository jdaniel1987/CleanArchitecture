using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface IGamesConsoleRepository
{
    Task<IReadOnlyCollection<GamesConsole>> GetAllGamesConsoles();
    Task<GamesConsole> GetGamesConsole(int gamesConsoleId);
    Task UpdateGamesConsole(GamesConsole gamesConsole);
    Task AddGamesConsole(GamesConsole gamesConsole);
    Task DeleteGamesConsole(int gamesConsoleId);
}
