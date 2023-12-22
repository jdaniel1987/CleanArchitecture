using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Queries.GetGamesForConsole;

public record GetGamesForConsoleResponse(IReadOnlyCollection<Game> Games);
