using Application.Features.Chat.DTOs;
using MediatR;

namespace Application.Features.Chat.CQRS.Queries
{
    public class ChatRequestQuery : IRequest<ChatRequestDto>
    {
        public string Message { get; set; }
        public string IpAddress { get; set; }
        public bool IsNewChat { get; set; }
    }
}
