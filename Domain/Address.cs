using Domain.Common;

namespace Domain
{
    public class Address : BaseDomainEntity
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string City { get; set; }
        public string SubCity { get; set; }
        public double longitude { get; set;}
        public double latitude { get; set;}
    }
}