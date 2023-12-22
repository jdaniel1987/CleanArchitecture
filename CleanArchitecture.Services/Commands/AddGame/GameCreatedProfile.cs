using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Services.EventHandlers.GameCreated;

namespace CleanArchitecture.Services.Commands.AddGame;

public class GameCreatedProfile : Profile
{
    public GameCreatedProfile()
    {
        CreateMap<Game, GameCreatedEvent>()
            .ForCtorParam(nameof(GameCreatedEvent.Game), opts => opts.MapFrom(src => src))
            .ForCtorParam(nameof(GameCreatedEvent.CreationDate), opts => opts.MapFrom(_ => DateTime.UtcNow));
    }
}
