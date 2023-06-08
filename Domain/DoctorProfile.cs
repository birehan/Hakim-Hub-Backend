using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public class DoctorProfile : BaseDomainEntity
    {
        public string FullName{get;set;}
        public string Email{get;set;}
        public string PhotoId { get; set; }
        public Photo Photo {get;set;}
        public DateTime TimeStamp {get;set;}

        public ICollection<InstitutionProfile> Institutions {get;set;}
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Speciality> Specialities { get; set; }

        
    }
}