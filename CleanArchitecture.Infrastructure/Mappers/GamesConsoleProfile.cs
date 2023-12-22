using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Infrastructure.Mappers;

public class GamesConsoleProfile : Profile
{
    public GamesConsoleProfile()
    {
        CreateMap<Models.GamesConsole, GamesConsole>()
            .ForCtorParam(nameof(GamesConsole.Id), opts => opts.MapFrom(src => src.Id))
            .ForCtorParam(nameof(GamesConsole.Name), opts => opts.MapFrom(src => src.Name))
            .ForCtorParam(nameof(GamesConsole.Manufacturer), opts => opts.MapFrom(src => src.Manufacturer))
            .ForCtorParam(nameof(GamesConsole.Price), opts => opts.MapFrom(src => Price.Create(src.Price)));

        CreateMap<GamesConsole, Models.GamesConsole>()
            .ForMember(nameof(Models.GamesConsole.Price), opts => opts.MapFrom(src => src.Price.Value));
    }
}
