using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Services.Commands.DeleteGame;

public class DeleteGameRoute : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Carter will create this URL for the API project, so we can split each route for each command
        app.MapDelete("api/DeleteGame/{GameId:int}", async (int GameId, IMediator mediator) =>
        {
            return await mediator.Send(new DeleteGameCommand(GameId));
        })
        .WithName(nameof(DeleteGameRoute))
        .WithTags(nameof(Domain.Entities.Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent);
    }
}
