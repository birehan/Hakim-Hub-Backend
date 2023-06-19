using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class GetDoctorProfileListBySpecialityIdQuery : IRequest<Result<List<DoctorProfileDto>>>
    {
        public Guid SpecialityId { get; set; }
    }
}