using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using AutoMapper;

namespace ApiChallenge.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<CreateVideoDto, Video>();
            CreateMap<UpdateVideoDto, Video>();
            CreateMap<Video, UpdateVideoDto>();
            CreateMap<Video, ReadVideoDto>();
            CreateMap<Video, UpdateVideoDto>();
            CreateMap<Video, List<Video>>();
            CreateMap<List<Video>, ReadVideoDto>();
        }
    }
}
