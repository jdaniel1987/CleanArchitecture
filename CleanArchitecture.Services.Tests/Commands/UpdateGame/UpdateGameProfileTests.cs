using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Services.Commands.UpdateGame;
using FluentAssertions;

namespace CleanArchitecture.Services.Tests.Commands.UpdateGame;

public class UpdateGameProfileTests
{
    private readonly IMapper _mapper;

    public UpdateGameProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UpdateGameCommandProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_game(
        UpdateGameCommand updateGameCommand)
    {
        var actual = _mapper.Map<Game>(updateGameCommand);

        var expected = new Game(
            updateGameCommand.Id,
            updateGameCommand.Name,
            updateGameCommand.Publisher,
            Price.Create(updateGameCommand.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
