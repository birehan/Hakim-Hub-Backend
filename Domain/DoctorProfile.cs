using System.Text.Json.Serialization;
using Domain.Common;
namespace Domain;



public class DoctorProfile : BaseDomainEntity
{
     public enum GenderType
        {
            Male,
            Female
        }

        public string FullName { get; set; }
        public string? About { get; set; }
        public GenderType Gender { get; set; }
        public string? Email { get; set; }
        public string? PhotoId { get; set; }
        [JsonIgnore]
        public Photo? Photo { get; set; }
        public DateTime CareerStartTime { get; set; }
        public Guid? MainInstitutionId { get; set; }
        [JsonIgnore]
        public InstitutionProfile? MainInstitution { get; set; }
        [JsonIgnore]
        public ICollection<InstitutionProfile> Institutions { get; set; } = new List<InstitutionProfile>();
        [JsonIgnore]
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();
        [JsonIgnore]
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        [JsonIgnore]
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        [JsonIgnore]
        public ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
}

