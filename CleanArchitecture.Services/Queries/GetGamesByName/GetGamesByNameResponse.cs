using CleanArchitecture.Domain.Entities.Aggregates;

namespace CleanArchitecture.Services.Queries.GetGamesByName;

public record GetGamesByNameResponse(IReadOnlyCollection<GameWithConsole> Games);
