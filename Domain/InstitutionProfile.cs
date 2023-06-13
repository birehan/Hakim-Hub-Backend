using System.Text.Json.Serialization;
using Domain.Common;
namespace Domain
{
    public class InstitutionProfile : BaseDomainEntity
    {
        public string InstitutionName { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 79f95ec (feat(search-biruk): add search and filter for institutionProfile)
        public string? BranchName { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string Summary { get; set; }
        public DateTime EstablishedOn { get; set; }
        public double Rate { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD

        [JsonIgnore]
        public Address? Address { get; set; }
        public string LogoId { get; set; }
        [JsonIgnore]
=======
        public Guid AddressId { get; set; }
<<<<<<< HEAD
=======
        public string BranchName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}
        

        // Foreign Keys
        public string LogoId {get;set;}
        public string BannerId { get; set; }

        // One to one realtion references
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
=======
        public Guid AddressId { get; set; }
>>>>>>> 79f95ec (feat(search-biruk): add search and filter for institutionProfile)
=======

        [JsonIgnore]
>>>>>>> 95d003c (fix(clean-biruk): clean up)
        public Address Address { get; set; }
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
<<<<<<< HEAD

<<<<<<< HEAD
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
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
=======
        [JsonIgnore]
        public InstitutionAvailability InstitutionAvailability { get; set; }
>>>>>>> 79f95ec (feat(search-biruk): add search and filter for institutionProfile)

    }
}