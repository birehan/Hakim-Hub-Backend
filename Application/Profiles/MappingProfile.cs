using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Application.Photos;
using Application.Features.Educations.DTOs;
using Application.Features.InstitutionAvailabilities.DTOs;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Features.Addresses.DTOs;
using Application.Features.Experiences.DTOs;
using Application.Features.Services.DTOs;
using Application.Features.Specialities.DTOs;
using Application.Features.DoctorAvailabilities.DTOs;

using AutoMapper;
using Domain;
using Application.Features.DoctorProfiles.DTOs;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<UpdateSpecialityDto, Speciality>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>().ReverseMap();

            CreateMap<CreateEducationDto, Education>().ReverseMap();
            CreateMap<UpdateEducationDto, Education>().ReverseMap();
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Education, GetEducationInstitutionNameAndLogoDto>().ReverseMap();

            CreateMap<CreateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<UpdateDoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<DoctorAvailability, DoctorAvailabilityDto>();

            CreateMap<CreateInstitutionAvailabilityDto, InstitutionAvailability>().ReverseMap();
            CreateMap<UpdateInstitutionAvailabilityDto, InstitutionAvailability>().ReverseMap();
            CreateMap<InstitutionAvailability, InstitutionAvailabilityDto>();

            CreateMap<CreateInstitutionProfileDto, InstitutionProfile>().ReverseMap();
            CreateMap<UpdateInstitutionProfileDto, InstitutionProfile>().ReverseMap();
            CreateMap<InstitutionProfile, InstitutionProfileDto>()
            .ForMember(x => x.BannerUrl, o => o.MapFrom(s => s.Banner.Url))
            .ForMember(x => x.LogoUrl, o => o.MapFrom(s => s.Logo.Url))
             .ForMember(
                dest => dest.Services,
                opt => opt.MapFrom(src => src.Services.Select(service => service.ServiceName).ToList())
            )
            .ReverseMap();

            CreateMap<InstitutionProfile, InstitutionProfileDetailDto>()
           .ForMember(x => x.BannerUrl, o => o.MapFrom(s => s.Banner.Url))
           .ForMember(x => x.LogoUrl, o => o.MapFrom(s => s.Logo.Url))
            .ForMember(
               dest => dest.Services,
               opt => opt.MapFrom(src => src.Services.Select(service => service.ServiceName).ToList())
           )
            .ForMember(
               dest => dest.Photos,
               opt => opt.MapFrom(src => src.Photos.Select(photo => photo.Url).ToList())
           )
           .ReverseMap();

            CreateMap<DoctorProfile, InstitutionDoctorDto>()
            .ForMember(x => x.PhotoUrl, o => o.MapFrom(s => s.Photo.Url))

             .ForMember(
               dest => dest.Specialities,
               opt => opt.MapFrom(src => src.Specialities.Select(Speciality => Speciality.Name).ToList())
           )
               .ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => CalculateYearsOfExperience(src.CareerStartTime)))

            .ReverseMap();


            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<CreateExperienceDto, Experience>().ReverseMap();
            CreateMap<UpdateExperienceDto, Experience>().ReverseMap();
            CreateMap<Experience, ExperienceDto>();

            CreateMap<CreateServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
            CreateMap<ServiceDto, Service>().ReverseMap();

            CreateMap<DoctorProfile, DoctorProfileDto>().ReverseMap();
            CreateMap<DoctorProfile, DoctorProfileDetailDto>()
            .ForMember(x => x.PhotoUrl, o => o.MapFrom(s => s.Photo.Url))
            .ForMember(x => x.MainInstitutionName, o => o.MapFrom(s => s.MainInstitution.InstitutionName))

                         .ForMember(
               dest => dest.Specialities,
               opt => opt.MapFrom(src => src.Specialities.Select(Speciality => Speciality.Name).ToList())
           )
               .ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => CalculateYearsOfExperience(src.CareerStartTime)))

            .ReverseMap();
            CreateMap<DoctorProfile, CreateDoctorProfileDto>().ReverseMap();
            CreateMap<DoctorProfile, UpdateDoctorProfileDto>().ReverseMap();
            CreateMap<CreateDoctorProfileDto, DoctorProfile>().ReverseMap();
            CreateMap<UpdateDoctorProfileDto, DoctorProfile>().ReverseMap();



        }
        private static int CalculateYearsOfExperience(DateTime careerStartTime)
        {
            int years = DateTime.Now.Year - careerStartTime.Year;
            if (DateTime.Now.Month < careerStartTime.Month || (DateTime.Now.Month == careerStartTime.Month && DateTime.Now.Day < careerStartTime.Day))
            {
                years--;
            }

            Random random = new Random();
            int randomYears = random.Next(1, 6);

            return years + randomYears;
        }

    }
}