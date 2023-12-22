using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleCommandProfile : Profile
{
    public UpdateGamesConsoleCommandProfile()
    {
        CreateMap<UpdateGamesConsoleCommand, GamesConsole>();
    }
}
