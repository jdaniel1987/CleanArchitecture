using AutoFixture.Xunit2;
using AutoFixture;
using AutoMapper;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Services.Commands.AddGame;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;

namespace CleanArchitecture.Services.Tests.Commands.AddGame;

public class AddGamesConsoleProfileTests
{
    private readonly IMapper _mapper;

    public AddGamesConsoleProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AddGameCommandProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_game(
        AddGameCommand addGameCommand)
    {
        var actual = _mapper.Map<Game>(addGameCommand);

        var expected = new Game(
            0,
            addGameCommand.Name,
            addGameCommand.Publisher,
            Price.Create(addGameCommand.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
