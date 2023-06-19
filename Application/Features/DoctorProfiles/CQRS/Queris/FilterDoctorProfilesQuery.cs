using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class FilterDoctorProfilesQuery : IRequest<Result<List<DoctorProfileDetailDto>>>
    {
        public string? SpecialityName { get; set; }
        public Guid? InstitutionId { get; set; }
        public int ExperienceYears { get; set; }
        public string? EducationName { get; set; }
    }

}