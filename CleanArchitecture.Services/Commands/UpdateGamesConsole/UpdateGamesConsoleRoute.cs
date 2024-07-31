using Carter;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Services.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Carter will create this URL for the API project, so we can split each route for each command
        app.MapPut("api/UpdateGamesConsole", async (IMediator mediator, UpdateGamesConsoleCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(UpdateGamesConsoleRoute))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK);
    }
}
