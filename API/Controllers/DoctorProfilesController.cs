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
using Microsoft.AspNetCore.Mvc;
using static Domain.DoctorProfile;

namespace API.Controllers
{

    public class DoctorProfilesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<DoctorProfileDetailDto>>> GetDoctorProfileDetail(Guid id)
        {
            var query = new GetDoctorProfileDetialQuery { Id = id };
            var response = await Mediator.Send(query);
            return HandleResult<DoctorProfileDetailDto>(response);


        }
        // [HttpGet]
        // public async Task<ActionResult<Result<List<DoctorProfileDto>>>> GetDoctors()
        // {
        //     var query = new GetDoctorProfileListQuery();
        //     var response = await Mediator.Send(query);
        //     return HandleResult<List<DoctorProfileDto>>(response);
        // }

        [HttpGet("specialities/{specialityId}")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesBySpecialityID(Guid specialityId)
        {
            var query = new GetDoctorProfileListBySpecialityIdQuery { SpecialityId = specialityId };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);
        }

       [HttpGet]
       public async Task<ActionResult<List<DoctorProfileDto>>> FilterDoctors(Guid specialityID, Guid educationId, DateTime careerStartTime, Guid institutionId){
        var query = new FilterDoctorProfilesQuery{specialityId=specialityID,EducationId=educationId,careerStartTime=careerStartTime,institutionId=institutionId};
        var response = await Mediator.Send(query);
        return HandleResult<List<DoctorProfileDto>>(response);
       }
        

        [HttpGet("institution/{institutionId}")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesByInstitutionId(Guid institutionId)
        {
            var query = new GetDoctorProfileListByInstitutionIdQuery { InstitutionId = institutionId };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);
        }

        [HttpGet("gender")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesByGender(GenderType gender)
        {
            var query = new GetDoctorProfileListByGenderQuery { Gender = gender };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);
        }
        [HttpGet("exprience/{careerStartTime}")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesByCareerStartTime(DateTime careerStartTime)
        {
            var query = new GetDoctorProfileListByCareerStartTimeQuery { CareerStartTime = careerStartTime };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);


        }
        [HttpGet("speciality/{specialityId}/gender/{gender}")]
        public async Task<ActionResult<List<DoctorProfileDto>>> GetDoctorProfilesBySpecialityIdAndGender(Guid specialityId, GenderType gender)
        {

            var query = new GetDoctorProfileListBySpecialityAndGenderQuery { specialityId = specialityId, Gender = gender };
            var response = await Mediator.Send(query);
            return HandleResult<List<DoctorProfileDto>>(response);

        }
        [HttpPost("createDoctorProfile")]
        public async Task<ActionResult<Guid>> Post([FromBody] CreateDoctorProfileDto createDoctorProfileDto)
        {
            var command = new CreateDoctorProfileCommand { CreateDoctorProfileDto = createDoctorProfileDto };
            var response = await Mediator.Send(command);
            return HandleResult<Guid>(response);
        }

        [HttpPatch]
        public async Task<ActionResult<Unit>> Update([FromBody] UpdateDoctorProfileDto updateDoctorProfileDto)
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