using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Mappers;
using FluentAssertions;
using GameDomain = CleanArchitecture.Domain.Entities.Game;
using GameModel = CleanArchitecture.Infrastructure.Models.Game;
using GamesConsoleDomain = CleanArchitecture.Domain.Entities.GamesConsole;
using GamesConsoleModel = CleanArchitecture.Infrastructure.Models.GamesConsole;

namespace CleanArchitecture.Infrastructure.Tests.Mappers;

public class GameWithConsoleProfileTests
{
    private readonly IMapper _mapper;

    public GameWithConsoleProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GameProfile());
            mc.AddProfile(new GamesConsoleProfile());
            mc.AddProfile(new GameWithConsoleProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_aggregate(
        IFixture fixture)
    {
        var gameModel = fixture.Build<GameModel>()
            .With(g => g.GamesConsole, fixture.Build<GamesConsoleModel>()
                .Without(c => c.Games)
                .Create())
            .Create();

        var actual = _mapper.Map<GameWithConsole>(gameModel);

        var expected = new GameWithConsole(
            new GameDomain(
                gameModel.Id,
                gameModel.Name,
                gameModel.Publisher,
                Price.Create(gameModel.Price)),
            new GamesConsoleDomain(
                gameModel.GamesConsole.Id,
                gameModel.GamesConsole.Name,
                gameModel.GamesConsole.Manufacturer,
                Price.Create(gameModel.GamesConsole.Price)));

        actual.Should().BeEquivalentTo(expected);
    }
}
