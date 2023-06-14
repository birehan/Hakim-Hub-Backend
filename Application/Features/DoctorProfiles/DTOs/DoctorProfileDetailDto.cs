using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Common;
using Domain;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class DoctorProfileDetailDto :BaseDto, IDoctorProfileDto
    {
        public string FullName{get;set;}
        public string About{get;set;}
        public string Email{get;set;}
        public DateTime CareerStartTime {get;set;}
        public GenderType Gender{get;set;}
        
        public Photo Photo {get;set;}

        public ICollection<InstitutionProfile> Institutions {get;set;}

        public InstitutionProfile MainInstitution {get;set;}

        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
    }
}