using CleanArchitecture.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

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
