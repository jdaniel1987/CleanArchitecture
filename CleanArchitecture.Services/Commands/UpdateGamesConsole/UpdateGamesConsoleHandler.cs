using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Services.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleHandler : IRequestHandler<UpdateGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository;
    private readonly IMapper _mapper;

    public UpdateGamesConsoleHandler(
        IGamesConsoleRepository gamesConsoleRepository,
        IMapper mapper)
    {
        _gamesConsoleRepository = gamesConsoleRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(UpdateGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var game = _mapper.Map<GamesConsole>(request);
        await _gamesConsoleRepository.UpdateGamesConsole(game);

        return Results.Ok();
    }
}
