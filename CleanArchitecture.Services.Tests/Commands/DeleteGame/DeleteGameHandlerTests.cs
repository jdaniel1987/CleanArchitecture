using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.DeleteGame;

namespace CleanArchitecture.Services.Tests.Commands.DeleteGame;

public class DeleteGameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_delete_game(
        Mock<IGameRepository> gameRepositoryMock,
        DeleteGameCommand deleteGameCommand)
    {
        var handler = new DeleteGameHandler(
            gameRepositoryMock.Object
        );

        var result = await handler.Handle(deleteGameCommand, CancellationToken.None);

        gameRepositoryMock.Verify(x => x.DeleteGame(deleteGameCommand.GameId), Times.Once);
        result.Should().BeOfType<Ok>();
    }
}
