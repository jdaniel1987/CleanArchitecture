using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Mappers;
using FluentAssertions;
using GameDomain = CleanArchitecture.Domain.Entities.Game;
using GameModel = CleanArchitecture.Infrastructure.Models.Game;

namespace CleanArchitecture.Infrastructure.Tests.Mappers;

public class GameProfileTests
{
    private readonly IMapper _mapper;

    public GameProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GameProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_model(
        int gamesConsoleId,
        GameDomain gameDomain)
    {
        var actual = _mapper.Map<GameModel>((gamesConsoleId, gameDomain));

        var expected = new GameModel
        {
            Id = gameDomain.Id,
            Name = gameDomain.Name,
            Publisher = gameDomain.Publisher,
            GamesConsoleId = gamesConsoleId,
            Price = gameDomain.Price.Value,
        };

        actual.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public void Should_map_to_domain(
        IFixture fixture)
    {
        var gameModel = fixture.Build<GameModel>()
            .Without(g => g.GamesConsole)
            .Create();

        var actual = _mapper.Map<GameDomain>(gameModel);

        var expected = new GameDomain(
            gameModel.Id,
            gameModel.Name,
            gameModel.Publisher,
            Price.Create(gameModel.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
