using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult>;
