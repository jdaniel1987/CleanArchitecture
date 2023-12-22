using MediatR;

namespace CleanArchitecture.Services.Queries.GetGamesForConsole;

public record GetGamesForConsoleQuery(int GamesConsoleId) : IRequest<GetGamesForConsoleResponse>;
