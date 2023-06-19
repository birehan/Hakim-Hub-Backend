using Application.Features.Chat.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Chat.CQRS.Queries;


namespace API.Controllers
{
    public class ChatController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto requestDto)
        {
            var query = new ChatRequestQuery
            {
                Message = requestDto.Message,
                IpAddress = Request.Headers["IpAddress"],
                IsNewChat = requestDto.IsNewChat
            };

            var responseDto = await _mediator.Send(query);
            return HandleResult(responseDto);
        }
    }
};