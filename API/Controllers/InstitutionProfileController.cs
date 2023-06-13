using API.Controllers;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileDetailQuery { Id = id }));
        }

        [HttpGet("by-years/{years}")]
        public async Task<IActionResult> GetByYears(int years)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileByOprationYearsQuery { Years = years }));
        }

        [HttpGet("by-service/{serviceId}")]
        public async Task<IActionResult> GetByService(Guid serviceId)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileByServiceQuery { ServiceId = serviceId }));
        }

        [HttpGet("search-institutions")]
        public async Task<IActionResult> Search(string? serviceName = null, int operationYears = -1, bool openStatus = false)
        {
            return HandleResult(await _mediator.Send(new InstitutionProfileSearchQuery { ServiceName = serviceName, OperationYears = operationYears, OpenStatus = openStatus }));
        }

        [HttpGet("search-by-name")]
        public async Task<IActionResult> SearchByName(string Name)
        {
            return HandleResult(await _mediator.Send(new InstitutionProfileSearchByNameQuery { Name = Name }));
        }

    }
}