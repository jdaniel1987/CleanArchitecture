﻿using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class GamesConsoleRepository(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IMapper mapper) : IGamesConsoleRepository
{
    private readonly IDbContextFactory<DatabaseContext> _databaseContextFactory = databaseContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<GamesConsole>> GetAllGamesConsoles()
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var GamesConsoles = await databaseContext
            .GamesConsoles
            .ToArrayAsync();

        return _mapper.Map<IReadOnlyCollection<GamesConsole>>(GamesConsoles);
    }

    public async Task<GamesConsole> GetGamesConsole(int gamesConsoleId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gamesConsole = await databaseContext
            .GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsoleId);

        return _mapper.Map<GamesConsole>(gamesConsole);
    }

    public async Task AddGamesConsole(GamesConsole gamesConsole)
    {
        var gamesConsoleModel = _mapper.Map<Models.GamesConsole>(gamesConsole);

        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        await databaseContext.AddAsync(gamesConsoleModel);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateGamesConsole(GamesConsole gamesConsole)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var existingGamesConsoleModel = await databaseContext
            .GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsole.Id)
            ?? throw new Exception("Games console does not exist.");

        var updatedGamesConsole = _mapper.Map<Models.GamesConsole>(gamesConsole);

        existingGamesConsoleModel.Name = updatedGamesConsole.Name;
        existingGamesConsoleModel.Manufacturer = updatedGamesConsole.Manufacturer;
        existingGamesConsoleModel.Price = updatedGamesConsole.Price;
        await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteGamesConsole(int gamesConsoleId)
    {
        var databaseContext = await _databaseContextFactory.CreateDbContextAsync();
        var gamesConsoleToDelete = await databaseContext
            .GamesConsoles
            .FirstOrDefaultAsync(g => g.Id == gamesConsoleId)
            ?? throw new Exception("Games console does not exist.");

        databaseContext.GamesConsoles.Remove(gamesConsoleToDelete);
        await databaseContext.SaveChangesAsync();
    }
}
