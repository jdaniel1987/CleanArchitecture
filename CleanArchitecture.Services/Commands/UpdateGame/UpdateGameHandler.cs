using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Services.Commands.UpdateGame;

public class UpdateGameHandler : IRequestHandler<UpdateGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public UpdateGameHandler(
        IGameRepository gameRepository,
        IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = _mapper.Map<Game>(request);
        await _gameRepository.UpdateGame(game, request.GamesConsoleId);

        return Results.Ok();
    }
}
