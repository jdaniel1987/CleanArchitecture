using MediatR;

namespace CleanArchitecture.Services.Queries.GetAllGames;

public record GetAllGamesQuery() : IRequest<GetAllGamesResponse>;
