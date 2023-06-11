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
<<<<<<< HEAD

        [JsonIgnore]
        public Address? Address { get; set; }
        public string LogoId { get; set; }
        [JsonIgnore]
=======
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public string LogoId {get;set;}
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
        public Photo Logo { get; set; }
        public string BannerId { get; set; }
        [JsonIgnore]
        public Photo Banner { get; set; }
<<<<<<< HEAD
        [JsonIgnore]
        public ICollection<DoctorProfile> Doctors { get; set; } = new List<DoctorProfile>();

        [JsonIgnore]
        public ICollection<Services> Services { get; set; } = new List<Services>();
        [JsonIgnore]
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        [JsonIgnore]
=======

        public Guid InstitutionAvailabilityId {get; set;}
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
        public InstitutionAvailability InstitutionAvailability { get; set; }

    }
}