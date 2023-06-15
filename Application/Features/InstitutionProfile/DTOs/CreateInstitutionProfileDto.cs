using Application.Features.Addresses.DTOs;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Features.InstitutionProfiles.DTOs
{
    public class CreateInstitutionProfileDto : IInstitutionProfileDto
    {
        public string InstitutionName { get; set; }
        public string BranchName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber {get; set;}
        public string Summary {get; set;}
        public DateTime EstablishedOn {get; set;}
        public double Rate {get; set;}

        public IFormFile LogoFile {get;set;}
        public IFormFile BannerFile { get; set; }
        public string StringAddress { get; set; }

        public Photo? Logo { get; set; }
        public Photo? Banner { get; set; }
        public Guid? AddressId {get;set;}
        public string? LogoId {get;set;}
        public string? BannerId { get; set; }
    }
}