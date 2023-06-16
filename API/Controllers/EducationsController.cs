using System.Data;
using Application.Features.Educations.CQRS;
using Application.Features.Educations.CQRS.Queries;
using Application.Features.Educations.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EducationsController : BaseApiController
{
    private readonly IMediator _mediator;

    public EducationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<EducationDto>>> Get()
    {
        return HandleResult(await _mediator.Send(new GetEducationListQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return HandleResult(await _mediator.Send(new GetEducationDetailQuery { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreateEducationDto createEducationDto)
    {

        var command = new CreateEducationCommand { createEducationDto = createEducationDto };
        return HandleResult(await _mediator.Send(command));
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Put([FromForm] UpdateEducationDto updateEducationDto, Guid id)
    {
        updateEducationDto.Id = id;
        
        var command = new UpdateEducationCommand { updateEducationDto =  updateEducationDto};
        return HandleResult(await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteEducationCommand { Id = id };
        return HandleResult(await _mediator.Send(command));
    }

}
