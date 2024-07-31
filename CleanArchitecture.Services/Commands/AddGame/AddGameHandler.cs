using AutoMapper;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Events;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.AddGame;

public class AddGameHandler : IRequestHandler<AddGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _mediatorPublisher;

    public AddGameHandler(
        IGameRepository gameRepository,
        IMapper mapper,
        IPublisher publisher)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _mediatorPublisher = publisher;
    }

    public async Task<IResult> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var game = _mapper.Map<Domain.Entities.Game>(request);
        await _gameRepository.AddGameToConsole(request.GamesConsoleId, game);

        var gameCreatedEvent = _mapper.Map<GameCreatedEvent>(game);
        await _mediatorPublisher.Publish(gameCreatedEvent);

        return Results.Created($"api/Games/{game.Id}", null);
    }
}
