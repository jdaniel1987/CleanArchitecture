using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.EmailSender;
using CleanArchitecture.Services.Commands.AddGame;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Net.Http.Json;

namespace CleanArchitecture.API.IntegrationTests.Commands;

public class AddGameIntegrationTests
{
    [Theory, AutoData]
    public async Task Should_add_game(
        IFixture fixture,
        AddGameCommand addGameCommand,
        Mock<ILogger<FakeEmailSender>> mockLogger)
    {
        // Arrange
        await using var application = new CustomWebApplicationFactory(services =>
        {
            services
                    .Replace(ServiceDescriptor.Transient(_ => mockLogger.Object));
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

        var mapper = application.Services.GetService<IMapper>() !;

        // Act
        var response = await client.PostAsJsonAsync("api/AddGame", addGameCommand);

        // Assert
        var actual = await dbContext.Games.SingleAsync(g => g.GamesConsoleId == existingGamesConsole.Id);
        var expected = new Infrastructure.Models.Game()
        {
            Publisher = addGameCommand.Publisher,
            GamesConsoleId = addGameCommand.GamesConsoleId,
            Name = addGameCommand.Name,
            Price = addGameCommand.Price,
        };

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Should().BeEquivalentTo(expected, 
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GamesConsole));
        mockLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", "random@email.com"), Times.Once);
    }
}
