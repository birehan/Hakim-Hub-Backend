using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;

using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class FilterDoctorProfilesQuery : IRequest<Result<List<DoctorProfileDto>>>
    {
        public ICollection<string>? SpecialityNames { get; set; }
        public Guid? InstitutionId { get; set; }
        public int ExperienceYears { get; set; } = -1;
        public string? EducationName { get; set; } 
    }

}