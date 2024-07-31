using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Services.Commands.AddGamesConsole;
using FluentAssertions;

namespace CleanArchitecture.Services.Tests.Commands.AddGamesConsole;

public class AddGamesConsoleProfileTests
{
    private readonly IMapper _mapper;

    public AddGamesConsoleProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AddGamesConsoleCommandProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_games_console(
        AddGamesConsoleCommand addGamesConsoleCommand)
    {
        var actual = _mapper.Map<GamesConsole>(addGamesConsoleCommand);

        var expected = new GamesConsole(
            0,
            addGamesConsoleCommand.Name,
            addGamesConsoleCommand.Manufacturer,
            Price.Create(addGamesConsoleCommand.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
