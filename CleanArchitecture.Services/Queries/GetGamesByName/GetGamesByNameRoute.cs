﻿using Carter;
using CleanArchitecture.Domain.Entities.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Services.Queries.GetGamesByName;

public class GetGamesByNameConsolesRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesByName/{GameName}", (string GameName, IMediator mediator) =>
        {
            return mediator.Send(new GetGamesByNameQuery(GameName));
        })
        .WithName(nameof(GetGamesByNameConsolesRoute))
        .WithTags(nameof(GameWithConsole));
    }
}
