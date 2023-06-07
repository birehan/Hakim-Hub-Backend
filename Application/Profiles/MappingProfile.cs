using Application.Features.Specialities.DTOs;
using AutoMapper;
using Domain;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<UpdateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>();
        }
    }
}