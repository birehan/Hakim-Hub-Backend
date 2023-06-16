using Application.Features.Specialities.DTOs;
using Application.Features.DoctorAvailabilities.DTOs;
using AutoMapper;
using Domain;
using Application.Features.InstitutionAvailabilities.DTOs;

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

            CreateMap<CreateInstitutionAvailabilityDto, InstitutionAvailability>().ReverseMap();
            CreateMap<UpdateInstitutionAvailabilityDto, InstitutionAvailability>().ReverseMap();
            CreateMap<InstitutionAvailability, InstitutionAvailabilityDto>();
        }
    }
}