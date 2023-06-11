using Domain;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http;
=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)

namespace Application.Features.InstitutionProfiles.DTOs
{
    public interface IInstitutionProfileDto
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}
<<<<<<< HEAD
<<<<<<< HEAD

=======
        public Guid AddressId { get; set; }
=======

>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
        public Guid LogoId {get;set;}
        public Guid BannerId { get; set; }
        
        public Address Address { get; set; }
        public InstitutionAvailability InstitutionAvailability {get; set;}

        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
    }
}