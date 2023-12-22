using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.AddGame;

public record AddGameCommand(
    string Name,
    string Publisher,
    int GamesConsoleId,
    double Price) : IRequest<IResult>;
