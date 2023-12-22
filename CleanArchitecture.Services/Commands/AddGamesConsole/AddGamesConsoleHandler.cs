using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Services.Commands.AddGamesConsole;

public class AddGamesConsoleHandler : IRequestHandler<AddGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository;
    private readonly IMapper _mapper;

    public AddGamesConsoleHandler(
        IGamesConsoleRepository gamesConsoleRepository,
        IMapper mapper)
    {
        _gamesConsoleRepository = gamesConsoleRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(AddGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = _mapper.Map<GamesConsole>(request);
        await _gamesConsoleRepository.AddGamesConsole(gamesConsole);

        return Results.Created($"api/GamesConsole/{gamesConsole.Id}", null);
    }
}
