using Application.Features.Chat.Models;

namespace Application.Features.Chat.DTOs;

public class ApiResponse
{
    public Data? Data { get; set; }
    public Error? ErrorMessage { get; set; }
}
