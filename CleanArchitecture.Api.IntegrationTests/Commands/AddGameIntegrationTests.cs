using AutoFixture;
using AutoFixture.Xunit2;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Services.Commands.AddGame;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGameIntegrationTests
{
    public sealed class AddGameTests
    {
        [Theory, AutoData]
        public async Task Should_send_command(
            IFixture fixture,
            AddGameCommand addGameCommand,
            Mock<IPublisher> mediatorPublisher)
        {
            // Arrange
            await using var application = new CustomWebApplicationFactory(services =>
            {
                services
                    .Replace(ServiceDescriptor.Transient(_ => mediatorPublisher.Object));
            });
            using var client = application.CreateClient();

            var dbContextFactory = application.Services.GetService<IDbContextFactory<DatabaseContext>>() !;
            using var dbContext = dbContextFactory.CreateDbContext();

            var existingGamesConsole = fixture.Build<Infrastructure.Models.GamesConsole>()
                .With(c => c.Id, addGameCommand.GamesConsoleId)
                .Without(c => c.Games)
                .Create();
            await dbContext.GamesConsoles.AddAsync(existingGamesConsole);
            await dbContext.SaveChangesAsync();

            // Act
            var response = await client.PostAsJsonAsync("api/AddGame", addGameCommand);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
