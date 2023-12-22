using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Mappers;
using CleanArchitecture.Infrastructure.Models;
using GamesConsoleDomain = CleanArchitecture.Domain.Entities.GamesConsole;
using GamesConsoleModel = CleanArchitecture.Infrastructure.Models.GamesConsole;

namespace CleanArchitecture.Infrastructure.Tests.Mappers;

public class GamesConsoleProfileTests
{
    private readonly IMapper _mapper;

    public GamesConsoleProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new GamesConsoleProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_model(
        GamesConsoleDomain gamesConsoleDomain)
    {
        var actual = _mapper.Map<GamesConsoleModel>(gamesConsoleDomain);

        var expected = new GamesConsoleModel { 
            Id = gamesConsoleDomain.Id,
            Name = gamesConsoleDomain.Name,
            Manufacturer = gamesConsoleDomain.Manufacturer,
            Games = Array.Empty<Game>(),
            Price = gamesConsoleDomain.Price.Value,
        };
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public void Should_map_to_domain(
        IFixture fixture)
    {
        var gamesConsoleModel = fixture.Build<GamesConsoleModel>()
            .Without(g => g.Games)
            .Create();

        var actual = _mapper.Map<GamesConsoleDomain>(gamesConsoleModel);

        var expected = new GamesConsoleDomain(
            gamesConsoleModel.Id,
            gamesConsoleModel.Name,
            gamesConsoleModel.Manufacturer,
            Price.Create(gamesConsoleModel.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
