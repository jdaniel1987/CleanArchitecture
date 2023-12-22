namespace CleanArchitecture.Domain.Entities.Aggregates;

public record ConsoleWithGames(
    GamesConsole Console,
    IReadOnlyCollection<Game> Games);
