using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.CQRS.Commands;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.DoctorProfile;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    public class DoctorProfilesController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<DoctorProfileDetailDto>>> GetDoctorProfileDetail(Guid id)
        {
            var query = new GetDoctorProfileDetialQuery { Id = id };
            var response = await Mediator.Send(query);
            return HandleResult<DoctorProfileDetailDto>(response);


        }


        [HttpGet("specialities/{specialityId}")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesBySpecialityID(Guid specialityId)
        {
            var query = new GetDoctorProfileListBySpecialityIdQuery { SpecialityId = specialityId };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<DoctorProfileDetailDto>>> FilterDoctors(string? specialityName, string? educationName, int experienceYears, Guid institutionId)
        {
            var query = new FilterDoctorProfilesQuery
            {
                SpecialityName = specialityName,
                EducationName = educationName,
                ExperienceYears = experienceYears,
                InstitutionId = institutionId
            };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDetailDto>>(response);
        }
        [HttpPost("createDoctorProfile")]
        public async Task<ActionResult<Guid>> Post([FromForm] CreateDoctorProfileDto createDoctorProfileDto)
        {
            var command = new CreateDoctorProfileCommand { CreateDoctorProfileDto = createDoctorProfileDto };
            var response = await Mediator.Send(command);
            return HandleResult<Guid>(response);
        }

        [HttpPatch]
        public async Task<ActionResult<Unit>> Update([FromForm] UpdateDoctorProfileDto updateDoctorProfileDto)
        {
            var command = new UpdateDoctorProfileCommand { updateDoctorProfileDto = updateDoctorProfileDto };
            var response = await Mediator.Send(command);
            return HandleResult<Unit>(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {

            var command = new DeleteDoctorProfileCommand { Id = id };
            var response = await Mediator.Send(command);
            return HandleResult<Unit>(response);

        }




    }
}