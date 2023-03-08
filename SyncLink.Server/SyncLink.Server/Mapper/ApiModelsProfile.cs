using AutoMapper;
using SyncLink.Application.UseCases.Login;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Mapper;

public class ApiModelsProfile : Profile
{
    public ApiModelsProfile()
    {
        CreateMap<LoginDto, LoginRequest>();
    }
}