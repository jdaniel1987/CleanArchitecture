using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Services.Commands.AddGame;

public class AddGameCommandProfile : Profile
{
    public AddGameCommandProfile()
    {
        CreateMap<AddGameCommand, Game>()
            .ForCtorParam(nameof(Game.Id), opts => opts.MapFrom(_ => 0))
            .ForCtorParam(nameof(Game.Price), opts => opts.MapFrom(src => Price.Create(src.Price)));
    }
}
