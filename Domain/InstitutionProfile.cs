using System.Text.Json.Serialization;
using Domain.Common;
namespace Domain
{
    public class InstitutionProfile : BaseDomainEntity
    {
        public string InstitutionName { get; set; }
        public string? BranchName { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string Summary { get; set; }
        public DateTime EstablishedOn { get; set; }
        public double Rate { get; set; }

        [JsonIgnore]
        public Address? Address { get; set; }
        public string LogoId { get; set; }
        [JsonIgnore]
        public Photo Logo { get; set; }
        public string BannerId { get; set; }
        [JsonIgnore]
        public Photo Banner { get; set; }
        [JsonIgnore]
        public ICollection<DoctorProfile> Doctors { get; set; } = new List<DoctorProfile>();

        [JsonIgnore]
        public ICollection<Services> Services { get; set; } = new List<Services>();
        [JsonIgnore]
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        [JsonIgnore]
        public InstitutionAvailability InstitutionAvailability { get; set; }

    }
}