using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.AddGame;
using CleanArchitecture.Services.Events;

namespace CleanArchitecture.Services.Tests.Commands.AddGame;

public class AddGameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_add_game(
        Mock<IGameRepository> gameRepositoryMock,
        Mock<IMapper> mapperMock,
        Mock<IPublisher> publisherMock,
        AddGameCommand addGameCommand,
        Game mappedGame,
        GameCreatedEvent mappedGameCreatedEvent)
    {
        var handler = new AddGameHandler(
            gameRepositoryMock.Object,
            mapperMock.Object,
            publisherMock.Object
        );
        mapperMock.Setup(x => x.Map<Game>(addGameCommand)).Returns(mappedGame);
        mapperMock.Setup(x => x.Map<GameCreatedEvent>(mappedGame)).Returns(mappedGameCreatedEvent);

        var result = await handler.Handle(addGameCommand, CancellationToken.None);

        publisherMock.Verify(x => x.Publish(mappedGameCreatedEvent, It.IsAny<CancellationToken>()), Times.Once);
        gameRepositoryMock.Verify(x => x.AddGameToConsole(addGameCommand.GamesConsoleId, mappedGame), Times.Once);
        result.Should().BeOfType<Created>();
    }

    [Theory, AutoData]
    public async Task Handle_should_not_add_game_when_games_console_doesnt_exist(
    Mock<IGameRepository> gameRepositoryMock,
    Mock<IMapper> mapperMock,
    Mock<IPublisher> publisherMock,
    AddGameCommand addGameCommand,
    Game mappedGame)
    {
        var handler = new AddGameHandler(
            gameRepositoryMock.Object,
            mapperMock.Object,
            publisherMock.Object
        );
        mapperMock.Setup(x => x.Map<Game>(addGameCommand)).Returns(mappedGame);

        var action  = async () => await handler.Handle(addGameCommand, CancellationToken.None);

        await action.Should().ThrowAsync<Exception>("Console does not exist.");
    }
}
