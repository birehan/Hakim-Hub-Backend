using Application.Features.Chat.DTOs;

namespace Application.Contracts.Infrastructure;

public interface IChatRequestSender
{
    Task<ApiResponseDto> SendMessage(string message, string ipAddress, bool isNewChat);
}
