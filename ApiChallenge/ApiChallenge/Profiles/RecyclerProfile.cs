using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using AutoMapper;

namespace ApiChallenge.Profiles
{
    public class RecyclerProfile : Profile
    {
        public RecyclerProfile()
        {
            CreateMap<UpdateRecyclerDto, Recycler>();
            CreateMap<ReadRecyclerDto, Recycler>();
            CreateMap<Recycler, ReadRecyclerDto>();
        }
    }
}
