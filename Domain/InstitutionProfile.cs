using Domain.Common;

namespace Domain
{
    public class InstitutionProfile : BaseDomainEntity
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }

        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime StartDate {get; set;}
        public string Rate {get; set;}
        
       

        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public string LogoId {get;set;}
        public Photo Logo { get; set; }
        public string BannerId { get; set; }
        public Photo Banner { get; set; }


        public ICollection<InstitutionAvailability> InstitutionAvailabilities { get; set; }
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; }

    }
}