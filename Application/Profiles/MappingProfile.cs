using Application.Features.Specialities.DTOs;
using Application.Features.DoctorAvailabilities.DTOs;
using AutoMapper;
using Domain;
using Application.Features.Experiences.DTOs;
using Application.Features.Services.DTOs;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<UpdateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>();

            CreateMap<CreateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<UpdateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<DoctorAvailability, DoctorAvailabilityDto>();

            CreateMap<CreateExperienceDto, Experience>().ReverseMap();
            CreateMap<UpdateExperienceDto, Experience>().ReverseMap();
            CreateMap<Experience, ExperienceDto>();

            CreateMap<CreateServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
            CreateMap<ServiceDto, Service>().ReverseMap();
        }
    }
}