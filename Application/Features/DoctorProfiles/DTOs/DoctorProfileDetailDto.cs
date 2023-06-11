using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Common;
using Domain;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class DoctorProfileDetailDto :BaseDto, IDoctorProfileDto
    {
        public string FullName{get;set;}
        public string About{get;set;}
        public string Email{get;set;}
        public DateTime CareerStartTime {get;set;}
        public Gender Gender{get;set;}
        
        // public PhotoDto Photo {get;set;}

        // public ICollection<InstitutionProfileDto> Institutions {get;set;}

        // public InstitutionProfileDto MainInstitution {get;set;}

        // public ICollection<EducationDto> Educations { get; set; }
        // public ICollection<ExperienceDto> Experiences { get; set; }
        // public ICollection<SpecialityDto> Specialities { get; set; }
    }
}