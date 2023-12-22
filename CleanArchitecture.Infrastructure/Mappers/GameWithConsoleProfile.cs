using AutoMapper;
using CleanArchitecture.Domain.Entities.Aggregates;

namespace CleanArchitecture.Infrastructure.Mappers;

public class GameWithConsoleProfile : Profile
{
    public GameWithConsoleProfile()
    {
        CreateMap<Models.Game, GameWithConsole>()
            .ForCtorParam(nameof(GameWithConsole.Game), opt => opt.MapFrom(src => src))
            .ForCtorParam(nameof(GameWithConsole.Console), opt => opt.MapFrom(src => src.GamesConsole));
    }
}
