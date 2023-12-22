namespace CleanArchitecture.Domain.Entities.Aggregates;

public record GameWithConsole(
    Game Game,
    GamesConsole Console);
