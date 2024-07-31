using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Services.Commands.UpdateGamesConsole;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CleanArchitecture.Services.Tests.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_update_games_console(
        Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        Mock<IMapper> mapperMock,
        UpdateGamesConsoleCommand updateGamesConsoleCommand,
        GamesConsole mappedGamesConsole)
    {
        var handler = new UpdateGamesConsoleHandler(
            gamesConsoleRepositoryMock.Object,
            mapperMock.Object
        );
        mapperMock.Setup(x => x.Map<GamesConsole>(updateGamesConsoleCommand)).Returns(mappedGamesConsole);

        var result = await handler.Handle(updateGamesConsoleCommand, CancellationToken.None);

        gamesConsoleRepositoryMock.Verify(x => x.UpdateGamesConsole(mappedGamesConsole), Times.Once);
        result.Should().BeOfType<Ok>();
    }
}
