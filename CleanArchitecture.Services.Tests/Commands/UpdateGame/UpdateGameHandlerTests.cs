using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.UpdateGame;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CleanArchitecture.Services.Tests.Commands.UpdateGame;

public class UpdateGameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_update_game(
        Mock<IGameRepository> gameRepositoryMock,
        Mock<IMapper> mapperMock,
        UpdateGameCommand updateGameCommand,
        Game mappedGame)
    {
        var handler = new UpdateGameHandler(
            gameRepositoryMock.Object,
            mapperMock.Object
        );
        mapperMock.Setup(x => x.Map<Game>(updateGameCommand)).Returns(mappedGame);

        var result = await handler.Handle(updateGameCommand, CancellationToken.None);

        gameRepositoryMock.Verify(x => x.UpdateGame(mappedGame, updateGameCommand.GamesConsoleId), Times.Once);
        result.Should().BeOfType<Ok>();
    }
}
