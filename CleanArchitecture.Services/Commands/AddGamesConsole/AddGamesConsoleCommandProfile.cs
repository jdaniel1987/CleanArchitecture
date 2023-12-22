using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Services.Commands.AddGamesConsole;

public class AddGamesConsoleCommandProfile : Profile
{
    public AddGamesConsoleCommandProfile()
    {
        CreateMap<AddGamesConsoleCommand, GamesConsole>()
            .ForCtorParam(nameof(GamesConsole.Id), opts => opts.MapFrom(_ => 0))
            .ForCtorParam(nameof(GamesConsole.Price), opts => opts.MapFrom(src => Price.Create(src.Price)));
    }
}
