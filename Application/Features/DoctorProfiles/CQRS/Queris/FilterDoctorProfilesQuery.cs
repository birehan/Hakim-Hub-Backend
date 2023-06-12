using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class FilterDoctorProfilesQuery: IRequest<Result<List<DoctorProfileDto>>>
    {
        public Guid specialityId{get;set;}
        public Guid institutionId{get;set;}
        public DateTime careerStartTime{get;set;}
        public Guid EducationId{get;set;}
    }
}