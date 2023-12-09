using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using AutoMapper;

namespace ApiChallenge.Profiles
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<CreateServerDto, Server>();
            CreateMap<UpdateServerDto, Server>();
            CreateMap<Server, UpdateServerDto>();
            CreateMap<Server, ReadServerDto>();
        }
    }
}
