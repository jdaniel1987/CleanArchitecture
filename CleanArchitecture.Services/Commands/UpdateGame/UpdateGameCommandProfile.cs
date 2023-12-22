using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Commands.UpdateGame;

public class UpdateGameCommandProfile : Profile
{
    public UpdateGameCommandProfile()
    {
        CreateMap<UpdateGameCommand, Game>();
    }
}