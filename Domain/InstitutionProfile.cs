using Domain.Common;

namespace Domain
{
    public class InstitutionProfile : BaseDomainEntity
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
        public ICollection<string> Doctors { get; set; }
        public ICollection<string> Services { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set;}
        public ICollection<string> Pictures { get; set;}
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime StartDate {get; set;}
        public string AddressId {get; set;}
        public Address address {get; set;}
        public string Rate {get; set;}
        public ICollection<string> InstitutionAvailabilities {get; set;}

    }
}