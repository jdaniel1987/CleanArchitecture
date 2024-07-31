using Carter;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Services.Queries.GetAllGamesConsoles;

public class GetAllGamesConsolesRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesConsoles", (IMediator mediator) =>
        {
            return mediator.Send(new GetAllGamesConsolesQuery());
        })
        .WithName(nameof(GetAllGamesConsolesRoute))
        .WithTags(nameof(GamesConsole));
    }
}
