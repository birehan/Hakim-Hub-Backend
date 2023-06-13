using API.Controllers;
using Application.Features.Addresses.CQRS.Commands;
using Application.Features.Addresses.CQRS.Queries;
using Application.Features.Addresses.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AddressesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> Get()
        {
            return HandleResult(await _mediator.Send(new GetAddressListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetAddressDetailQuery { Id = id }));
        }

    }
}