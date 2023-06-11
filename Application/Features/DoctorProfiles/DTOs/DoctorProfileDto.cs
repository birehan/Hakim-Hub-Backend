using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Common;
using Domain;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class DoctorProfileDto : BaseDto
    {
        public string FullName { get; set; }
        public Photo photo { get; set; }


        // public ICollection<SpecialityDto> specialities{get;set;} 
        // public InstitutionProfileDto MainInstitution {get;set;}

    }
}