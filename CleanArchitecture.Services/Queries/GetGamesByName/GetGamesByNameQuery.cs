using MediatR;

namespace CleanArchitecture.Services.Queries.GetGamesByName;

public record GetGamesByNameQuery(string GameName) : IRequest<GetGamesByNameResponse>;
