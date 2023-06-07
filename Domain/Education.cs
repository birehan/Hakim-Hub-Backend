using Domain.Common;

namespace Domain;

public class Education : BaseDomainEntity
{
    public string DoctorProfileId { get; set; }
    public string InstitutionName { get; set; }
    public string InstitutionLogo { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string FieldOfStudy { get; set; }
    public string Description { get; set; }
}
