using Application.Features.Common;
using Domain.Common;
using static Domain.DoctorProfile;

namespace Domain
{
    public class InstitutionDoctorDto : BaseDto
    {

        public string FullName { get; set; }
        public string? About { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public int YearsOfExperience { get; set; }
        public Guid? MainInstitutionId { get; set; }
        public ICollection<string> Specialities { get; set; } = new List<string>();

    }
}