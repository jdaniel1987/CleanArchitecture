using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Services.Queries.GetAllGames;

public class GetAllGamesHandler : IRequestHandler<GetAllGamesQuery, GetAllGamesResponse>
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesHandler(
        IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GetAllGamesResponse> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        var gamesWithConsole = await _gameRepository.GetAllGames();

        return new GetAllGamesResponse(gamesWithConsole);
    }
}
