using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.DeleteGamesConsole;

public record DeleteGamesConsoleCommand(int GamesConsoleId) : IRequest<IResult>;
