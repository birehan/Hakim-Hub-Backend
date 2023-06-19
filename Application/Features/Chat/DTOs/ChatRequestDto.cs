namespace Application.Features.Chat.DTOs;

public class ChatRequestDto
{
    public string? Message { get; set; }
    public string? IpAddress { get; set; }
    public bool IsNewChat { get; set; }

}
