using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using Domain;
using MediatR;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class GetDoctorProfileListByGenderQuery : IRequest<Result<List<DoctorProfileDto>>>
    {
        public GenderType Gender { get; set; }
    }
}