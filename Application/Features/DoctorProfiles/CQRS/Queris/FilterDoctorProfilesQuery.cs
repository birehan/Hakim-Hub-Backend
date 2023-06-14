using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class FilterDoctorProfilesQuery : IRequest<Result<List<DoctorProfileDto>>>
{
    public string? SpecialityName { get; set; }
    public Guid InstitutionId { get; set; }
    public DateTime? CareerStartTime { get; set; }
    public string? EducationName { get; set; }
}

}