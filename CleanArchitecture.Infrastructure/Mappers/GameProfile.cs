using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Aggregates;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Infrastructure.Mappers;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<(int GamesConsoleId, Game Game), Models.Game>()
            .ForMember(nameof(Models.Game.Id), opts => opts.MapFrom(src => src.Game.Id))
            .ForMember(nameof(Models.Game.Name), opts => opts.MapFrom(src => src.Game.Name))
            .ForMember(nameof(Models.Game.Publisher), opts => opts.MapFrom(src => src.Game.Publisher))
            .ForMember(nameof(Models.Game.GamesConsoleId), opts => opts.MapFrom(src => src.GamesConsoleId))
            .ForMember(nameof(Models.Game.Price), opts => opts.MapFrom(src => src.Game.Price.Value));

        CreateMap<Models.Game, Game>()
            .ForCtorParam(nameof(Game.Id), opts => opts.MapFrom(src => src.Id))
            .ForCtorParam(nameof(Game.Name), opts => opts.MapFrom(src => src.Name))
            .ForCtorParam(nameof(Game.Publisher), opts => opts.MapFrom(src => src.Publisher))
            .ForCtorParam(nameof(Game.Price), opts => opts.MapFrom(src => Price.Create(src.Price)));
    }
}
