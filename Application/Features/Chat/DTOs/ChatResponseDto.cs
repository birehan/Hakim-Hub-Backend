using Application.Features.Chat.Models;
using Domain;

namespace Application.Features.Chat.DTOs;

public class ChatResponseDto
{
    public string? replyMessage { get; set; }
    public Error? ErrorMessage { get; set; }
    public List<DoctorProfile>? Doctors { get; set; }
    public List<InstitutionProfile>? Institutions { get; set; }
}
