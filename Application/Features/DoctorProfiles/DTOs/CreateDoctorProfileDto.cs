using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class CreateDoctorProfileDto
    {
        public string FullName { get ;set;}
        public string About {get;set;}
        public string Email { get ;set;}
        public Photo Photo { get ;set;}
        public DateTime CareerStartTime { get ;set;}
        public GenderType Gender { get ;set;}
        
    }
}