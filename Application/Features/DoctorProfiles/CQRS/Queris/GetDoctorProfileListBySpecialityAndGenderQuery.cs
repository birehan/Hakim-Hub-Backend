using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.DTOs;
using Application.Features.Specialities.DTOs;
using Application.Responses;
using Domain;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Queris
{
    public class GetDoctorProfileListBySpecialityAndGenderQuery: IRequest<Result<List<DoctorProfileDto>>>
    {
        public string speciality{get;set;}
        public string Gender{get;set;}
    }
}