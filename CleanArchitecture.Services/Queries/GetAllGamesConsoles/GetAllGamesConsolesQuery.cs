using MediatR;

namespace CleanArchitecture.Services.Queries.GetAllGamesConsoles;

public record GetAllGamesConsolesQuery() : IRequest<GetAllGamesConsolesResponse>;
