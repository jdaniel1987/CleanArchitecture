using MediatR;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Services.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleHandler : IRequestHandler<DeleteGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository;

    public DeleteGamesConsoleHandler(IGamesConsoleRepository gamesConsoleRepository)
    {
        _gamesConsoleRepository = gamesConsoleRepository;
    }

    public async Task<IResult> Handle(DeleteGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        await _gamesConsoleRepository.DeleteGamesConsole(request.GamesConsoleId);

        return Results.Ok();
    }
}
