namespace Application.Features.Educations.DTOs;

public interface IEducationDto
{
    public string EducationInstitution { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public Guid DoctorId { get; set; }
    public string? InstitutionLogoId { get; set; }
}
