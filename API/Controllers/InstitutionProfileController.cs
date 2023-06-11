using API.Controllers;
using Application.Features.InstitutionProfiles.CQRS.Commands;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Features.Specialities.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InsitutionProfileController : BaseApiController
    {
        private readonly IMediator _mediator;

        public InsitutionProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstitutionProfileDto>>> Get()
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileListQuery()));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInstitutionProfileDto createTask)
        {

            var command = new CreateInstitutionProfileCommand { CreateInstitutionProfileDto = createTask };
            return HandleResult(await _mediator.Send(command));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateInstitutionProfileDto specialityDto, Guid id)
        {
            specialityDto.Id = id;
            var command = new UpdateInstitutionProfileCommand { UpdateInstitutionProfileDto = specialityDto };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteInstitutionProfileCommand { Id = id };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileDetailQuery { Id = id }));
        }

    }
}