using Application.Features.Common;

namespace Application.Features.Educations.DTOs;

public class UpdateEducationDto : BaseDto, IEducationDto
{
    public string EducationInstitution { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public Guid DoctorId { get; set; }
    public string? InstitutionLogoId { get; set; }


}
