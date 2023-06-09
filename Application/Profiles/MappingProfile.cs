using Application.Features.Specialities.DTOs;
using Application.Features.DoctorAvailabilities.DTOs;
using AutoMapper;
using Domain;
using Application.Features.Educations.DTOs;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<UpdateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>();

            CreateMap<CreateEducationDto, Education>().ReverseMap();
            CreateMap<UpdateEducationDto, Education>().ReverseMap();
            CreateMap<Education, EducationDto>();

            CreateMap<CreateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<UpdateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<DoctorAvailability, DoctorAvailabilityDto>();
        }
    }
}