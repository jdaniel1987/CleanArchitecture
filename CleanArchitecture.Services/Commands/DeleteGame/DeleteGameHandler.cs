using MediatR;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Services.Commands.DeleteGame;

public class DeleteGameHandler : IRequestHandler<DeleteGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository;

    public DeleteGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IResult> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        await _gameRepository.DeleteGame(request.GameId);

        return Results.Ok();
    }
}
