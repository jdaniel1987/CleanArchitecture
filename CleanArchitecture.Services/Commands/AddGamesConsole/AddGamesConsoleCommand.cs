using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.AddGamesConsole;

public record AddGamesConsoleCommand(
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult>;
