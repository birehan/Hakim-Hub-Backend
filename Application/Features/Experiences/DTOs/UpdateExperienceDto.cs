using Application.Features.Common;

namespace Application.Features.Experiences.DTOs;

public class UpdateExperienceDto : BaseDto, IExperienceDto
{
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid DoctorId {get; set;}
    public Guid InstitutionId { get; set; }
}
