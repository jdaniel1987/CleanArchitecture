using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.UpdateGame;

public record UpdateGameCommand(
    int Id,
    string Name,
    string Publisher,
    int GamesConsoleId,
    double Price) : IRequest<IResult>;
