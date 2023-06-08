using System.ComponentModel;
using Domain.Common;

namespace Domain;

public class Education : BaseDomainEntity
{
    public string InstitutionName { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime GraduationYear { get; set; }
    public string FieldOfStudy { get; set; }
    public string Description { get; set; }

    public Guid DoctorId { get; set; }
    public string PhotoId { get; set; }
    public DoctorProfile Doctor { get; set; }
    public Photo InstitutionLogo { get; set; }
}
