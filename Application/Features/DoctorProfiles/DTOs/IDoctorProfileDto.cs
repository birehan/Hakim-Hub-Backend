using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.DTOs
{
    public interface IDoctorProfileDto
    {
        string FullName { get; set; }
        string About { get; set; }
        string Email { get; set; }

        DateTime CareerStartTime { get; set; }
        GenderType Gender { get; set; }
        // PhotoDto Photo {get;set;}

        // ICollection<InstitutionProfileDto> Institutions {get;set;}

        // InstitutionProfileDto MainInstitution {get;set;}

        // ICollection<EducationDto> Educations { get; set; }
        // ICollection<ExperienceDto> Experiences { get; set; }
        // ICollection<SpecialityDto> Specialities { get; set; }



    }
}