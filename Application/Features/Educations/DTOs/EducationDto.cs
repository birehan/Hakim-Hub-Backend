using Application.Features.Common;

namespace Application.Features.Educations.DTOs;

public class EducationDto : BaseDto, IEducationDto
{
    public string InstitutionName { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string FieldOfStudy { get; set; }
    public string Description { get; set; }
    public Guid DoctorId { get; set; }
    public string PhotoId { get; set; }
}
