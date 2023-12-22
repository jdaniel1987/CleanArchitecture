using CleanArchitecture.Domain.Entities.Aggregates;

namespace CleanArchitecture.Services.Queries.GetAllGames;

public record GetAllGamesResponse(IReadOnlyCollection<GameWithConsole> GamesWithConsole);
