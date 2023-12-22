﻿using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Commands.UpdateGame;

public class UpdateGameRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Carter will create this URL for the API project, so we can split each route for each command
        app.MapPut("api/UpdateGame", async (IMediator mediator, UpdateGameCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(UpdateGameRoute))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK);
    }
}
