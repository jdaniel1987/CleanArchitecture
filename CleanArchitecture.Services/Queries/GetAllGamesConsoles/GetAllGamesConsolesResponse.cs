using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Queries.GetAllGamesConsoles;

public record GetAllGamesConsolesResponse(IReadOnlyCollection<GamesConsole> GamesConsoles);
