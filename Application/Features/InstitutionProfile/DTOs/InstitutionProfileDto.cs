using Application.Features.Common;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Features.InstitutionProfiles.DTOs
{
    public class InstitutionProfileDto : BaseDto, IInstitutionProfileDto
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}

        public bool Status {get; set;}


        public IFormFile Logo {get;set;}
        public IFormFile Banner { get; set; }

        public InstitutionAvailability InstitutionAvailability {get; set;}
        public Address Address {get; set;}

        public ICollection<DoctorProfile> Doctors { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Photo> Photos { get; set; }

    }
}