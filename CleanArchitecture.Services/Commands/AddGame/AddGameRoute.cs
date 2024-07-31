using Carter;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Services.Commands.AddGame;

public class AddGameRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Carter will create this URL for the API project, so we can split each route for each command
        app.MapPost("api/AddGame", async (IMediator mediator, AddGameCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(AddGameRoute))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }
}
