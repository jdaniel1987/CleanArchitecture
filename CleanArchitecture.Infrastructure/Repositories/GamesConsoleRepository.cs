using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Infrastructure.Repositories;

public class GamesConsoleRepository : IGamesConsoleRepository
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;

    public GamesConsoleRepository(
        DatabaseContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GamesConsole>> GetAllGamesConsoles()
    {
        var GamesConsoles = await _dbContext.GamesConsoles.ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GamesConsole>>(GamesConsoles);
    }

    public async Task<GamesConsole> GetGamesConsole(int gamesConsoleId)
    {
        var gamesConsole = await _dbContext.GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsoleId);

        return _mapper.Map<GamesConsole>(gamesConsole);
    }

    public async Task AddGamesConsole(GamesConsole gamesConsole)
    {
        var gamesConsoleModel = _mapper.Map<Models.GamesConsole>(gamesConsole);

        await _dbContext.AddAsync(gamesConsoleModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateGamesConsole(GamesConsole gamesConsole)
    {
        var existingGamesConsoleModel = await _dbContext.GamesConsoles.FirstOrDefaultAsync(c => c.Id == gamesConsole.Id)
            ?? throw new Exception("Games console does not exist.");

        var updatedGamesConsole = _mapper.Map<Models.GamesConsole>(gamesConsole);

        existingGamesConsoleModel.Name = updatedGamesConsole.Name;
        existingGamesConsoleModel.Manufacturer = updatedGamesConsole.Manufacturer;
        existingGamesConsoleModel.Price = updatedGamesConsole.Price;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGamesConsole(int gamesConsoleId)
    {
        var gamesConsoleToDelete = await _dbContext.GamesConsoles
            .FirstOrDefaultAsync(g => g.Id == gamesConsoleId) ?? 
            throw new Exception("Games console does not exist.");

        _dbContext.GamesConsoles.Remove(gamesConsoleToDelete);
        await _dbContext.SaveChangesAsync();
    }
}
