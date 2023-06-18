using Application.Features.Specialities.DTOs;
using Application.Features.DoctorAvailabilities.DTOs;
using AutoMapper;
using Domain;

using Application.Features.InstitutionProfiles.DTOs;
using Application.Features.Addresses.DTOs;

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
            
            CreateMap<CreateInstitutionProfileDto, InstitutionProfile>().ReverseMap();
            CreateMap<UpdateInstitutionProfileDto, InstitutionProfile>().ReverseMap();
            CreateMap<InstitutionProfileDto, InstitutionProfile>().ReverseMap();

            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<CreateExperienceDto, Experience>().ReverseMap();
            CreateMap<UpdateExperienceDto, Experience>().ReverseMap();
            CreateMap<Experience, ExperienceDto>();

            CreateMap<CreateServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
            CreateMap<ServiceDto, Service>().ReverseMap();
        }
    }
}