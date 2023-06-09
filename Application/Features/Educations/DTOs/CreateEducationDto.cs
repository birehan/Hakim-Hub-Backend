namespace Application.Features.Educations.DTOs;

public class CreateEducationDto : IEducationDto
{
    public string InstitutionName { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string FieldOfStudy { get; set; }
    public string Description { get; set; }
    public Guid DoctorId { get; set; }
    public string PhotoId { get; set; }
}
