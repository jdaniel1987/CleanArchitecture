using AutoFixture;
using AutoFixture.Xunit2;
using CleanArchitecture.Services.Commands.AddGame;
using MediatR;
using Moq;
using System.Net.Http.Json;

namespace CleanArchitecture.Services.Tests.Commands.AddGame;

public class AddGameRouteTests
{
    public sealed class AddGame
    {
        [Theory, AutoData]
        public async Task Should_send_command(
            IFixture fixture,
            AddGameCommand addGameCommand,
            [Frozen] Mock<IMediator> mockMediator)
        {
            using var client = CustomClient.CreateCustomClient(fixture);

            var response = await client.PostAsJsonAsync("api/AddGame", addGameCommand);

            mockMediator.Verify(m => m.Send(addGameCommand, default), Times.Once);
        }
    }   
}
