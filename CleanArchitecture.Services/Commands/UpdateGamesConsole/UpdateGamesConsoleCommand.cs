using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.UpdateGamesConsole;

public record UpdateGamesConsoleCommand(
    int Id,
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult>;
