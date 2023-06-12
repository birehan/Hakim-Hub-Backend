using Application.Features.Common;
using Domain;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http;
=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)

namespace Application.Features.InstitutionProfiles.DTOs
{
    public class UpdateInstitutionProfileDto : BaseDto, IInstitutionProfileDto
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}
<<<<<<< HEAD
<<<<<<< HEAD

<<<<<<< HEAD
        public IFormFile? LogoFile {get;set;}
        public IFormFile? BannerFile { get; set; }
        public ICollection<IFormFile>? PhotoFiles { get; set; }
=======
        public Guid AddressId { get; set; }
=======

>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
        public Guid LogoId {get;set;}
        public Guid BannerId { get; set; }
=======
        public string LogoId {get;set;}
        public string BannerId { get; set; }
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

        public ICollection<InstitutionAvailability> InstitutionAvailabilities {get; set;}
        public Address Address { get; set; }


        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
    }
}