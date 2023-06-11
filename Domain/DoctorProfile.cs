using Domain.Common;
namespace Domain;

public enum Gender
{
    Male,
    Female
}

public class DoctorProfile : BaseDomainEntity
{
    public string FullName { get; set; }
    public string About { get; set; }
    public string Email { get; set; }
    public string PhotoId { get; set; }
    public Photo Photo { get; set; }
    public DateTime CareerStartTime { get; set; }
    public Gender Gender { get; set; }
    public ICollection<InstitutionProfile> Institutions { get; set; }
    public Guid MainInstitutionId { get; set; }
    public InstitutionProfile MainInstitution { get; set; }
    public ICollection<Education> Educations { get; set; }
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Speciality> Specialities { get; set; }


}
