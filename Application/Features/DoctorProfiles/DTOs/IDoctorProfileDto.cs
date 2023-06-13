
using static Domain.DoctorProfile;

public interface IDoctorProfileDto 
    {
        public string FullName{get;set;}
        public string About{get;set;}
        public string Email{get;set;}
        public DateTime CareerStartTime {get;set;}
        public GenderType Gender{get;set;}
        
        // public PhotoDto Photo {get;set;}

        // public ICollection<InstitutionProfileDto> Institutions {get;set;}

        // public InstitutionProfileDto MainInstitution {get;set;}

        // public ICollection<EducationDto> Educations { get; set; }
        // public ICollection<ExperienceDto> Experiences { get; set; }
        // public ICollection<SpecialityDto> Specialities { get; set; }
    }
