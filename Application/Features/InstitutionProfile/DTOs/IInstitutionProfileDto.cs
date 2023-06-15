using Domain;
using Microsoft.AspNetCore.Http;

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

        public IFormFile Logo {get;set;}
        public IFormFile Banner { get; set; }

    }
}