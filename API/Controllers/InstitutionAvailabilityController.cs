using API.Controllers;
using Application.Features.InstitutionAvailabilities.CQRS.Commands;
using Application.Features.InstitutionAvailabilities.CQRS.Queries;
using Application.Features.InstitutionAvailabilities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class InstitutionAvailabilityController : BaseApiController
    {
        private readonly IMediator _mediator;

        public InstitutionAvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstitutionAvailabilityDto>>> Get()
        {
            return HandleResult(await _mediator.Send(new GetInstitutionAvailabilityListQuery()));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInstitutionAvailabilityDto createTask)
        {

            var command = new CreateInstitutionAvailabilityCommand { CreateInstitutionAvailabilityDto = createTask };
            return HandleResult(await _mediator.Send(command));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateInstitutionAvailabilityDto institutionAvailabilityDto, Guid id)
        {
            institutionAvailabilityDto.Id = id;
            var command = new UpdateInstitutionAvailabilityCommand { UpdateInstitutionAvailabilityDto = institutionAvailabilityDto };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteInstitutionAvailabilityCommand { Id = id };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionAvailabilityDetailQuery { Id = id }));
        }
