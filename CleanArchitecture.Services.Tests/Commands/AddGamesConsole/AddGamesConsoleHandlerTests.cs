using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.AddGamesConsole;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CleanArchitecture.Services.Tests.Commands.AddGamesConsole;

public class AddGamesconsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_add_games_console(
        Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        Mock<IMapper> mapperMock,
        AddGamesConsoleCommand addGamesConsoleCommand,
        GamesConsole mappedGamesConsole)
    {
        var handler = new AddGamesConsoleHandler(
            gamesConsoleRepositoryMock.Object,
            mapperMock.Object
        );
        mapperMock.Setup(x => x.Map<GamesConsole>(addGamesConsoleCommand)).Returns(mappedGamesConsole);

        var result = await handler.Handle(addGamesConsoleCommand, CancellationToken.None);

        gamesConsoleRepositoryMock.Verify(x => x.AddGamesConsole(mappedGamesConsole), Times.Once);
        result.Should().BeOfType<Created>();
    }
}
