using Application.Features.Common;
using Domain;

namespace Application.Features.Addresses.DTOs
{
    public class AddressDto : BaseDto, IAddressDto
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string City { get; set; }
        public string SubCity { get; set; }
        public double Longitude { get; set;}
        public double Latitude { get; set;}
<<<<<<< HEAD
        public string Summary { get; set; }


        public string InstitutionId {get; set;}
=======

        public Guid InstitutionId {get; set;}
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
        public InstitutionProfile Institution {get; set;}

    }
}