using AutoFixture.Xunit2;
using AutoFixture;
using AutoMapper;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using CleanArchitecture.Services.Commands.UpdateGamesConsole;

namespace CleanArchitecture.Services.Tests.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleProfileTests
{
    private readonly IMapper _mapper;

    public UpdateGamesConsoleProfileTests()
    {
        var fixture = new Fixture();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UpdateGamesConsoleCommandProfile());
        });

        _mapper = new Mapper(mappingConfig);
        fixture.Register(() => _mapper);
    }

    [Theory, AutoData]
    public void Should_map_to_game(
        UpdateGamesConsoleCommand updateGamesConsoleCommand)
    {
        var actual = _mapper.Map<GamesConsole>(updateGamesConsoleCommand);

        var expected = new GamesConsole(
            updateGamesConsoleCommand.Id,
            updateGamesConsoleCommand.Name,
            updateGamesConsoleCommand.Manufacturer,
            Price.Create(updateGamesConsoleCommand.Price));

        actual.Should().BeEquivalentTo(expected);
    }
}
