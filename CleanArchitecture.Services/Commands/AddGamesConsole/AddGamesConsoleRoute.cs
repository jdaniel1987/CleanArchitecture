using Carter;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Services.Commands.AddGamesConsole;

public class AddGamesConsoleRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Carter will create this URL for the API project, so we can split each route for each command
        app.MapPost("api/AddGamesConsole", async (IMediator mediator, AddGamesConsoleCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(AddGamesConsoleRoute))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }
}
