using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.DeleteGamesConsole;

namespace CleanArchitecture.Services.Tests.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_delete_games_console(
        Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        DeleteGamesConsoleCommand deleteGamesConsoleCommand)
    {
        var handler = new DeleteGamesConsoleHandler(
            gamesConsoleRepositoryMock.Object
        );

        var result = await handler.Handle(deleteGamesConsoleCommand, CancellationToken.None);

        gamesConsoleRepositoryMock.Verify(x => x.DeleteGamesConsole(deleteGamesConsoleCommand.GamesConsoleId), Times.Once);
        result.Should().BeOfType<Ok>();
    }
}
