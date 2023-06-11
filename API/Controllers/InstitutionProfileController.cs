using API.Controllers;
using Application.Features.InstitutionProfiles.CQRS.Commands;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Features.Specialities.CQRS.Queries;
<<<<<<< HEAD
using Application.Interfaces;
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InsitutionProfileController : BaseApiController
    {
        private readonly IMediator _mediator;
<<<<<<< HEAD
        private readonly IPhotoAccessor _photoAccessor;
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)

        public InsitutionProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstitutionProfileDto>>> Get()
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileListQuery()));
        }

<<<<<<< HEAD
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

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateInstitutionProfileDto createTask)
        {
=======

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInstitutionProfileDto createTask)
        {

>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
            var command = new CreateInstitutionProfileCommand { CreateInstitutionProfileDto = createTask };
            return HandleResult(await _mediator.Send(command));
        }


<<<<<<< HEAD
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put([FromForm] UpdateInstitutionProfileDto InstitutionProfileDto, Guid id)
        {
            InstitutionProfileDto.Id = id;
            var command = new UpdateInstitutionProfileCommand { UpdateInstitutionProfileDto = InstitutionProfileDto };
            return HandleResult(await _mediator.Send(command));
        }
        
=======
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateInstitutionProfileDto specialityDto, Guid id)
        {
            specialityDto.Id = id;
            var command = new UpdateInstitutionProfileCommand { UpdateInstitutionProfileDto = specialityDto };
            return HandleResult(await _mediator.Send(command));
        }
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteInstitutionProfileCommand { Id = id };
            return HandleResult(await _mediator.Send(command));
        }

<<<<<<< HEAD
=======
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetInstitutionProfileDetailQuery { Id = id }));
        }

>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
    }
}