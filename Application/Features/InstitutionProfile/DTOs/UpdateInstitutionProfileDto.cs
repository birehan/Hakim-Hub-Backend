using Application.Features.Common;
using Domain;

namespace Application.Features.InstitutionProfiles.DTOs
{
    public class UpdateInstitutionProfileDto : BaseDto, IInstitutionProfileDto
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}

        public Guid LogoId {get;set;}
        public Guid BannerId { get; set; }

        public InstitutionAvailability InstitutionAvailability {get; set;}
        public Address Address { get; set; }


        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}