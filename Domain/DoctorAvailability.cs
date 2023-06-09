using Domain.Common;

namespace Domain
{
    public class DoctorAvailability: BaseDomainEntity
    {

        public bool TwentyFourHours { get; set; }
        public DayOfWeek StartDay {get; set;}

        public DayOfWeek EndDay {get; set;}

        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}

        public Guid DoctorId { get; set; }
        public DoctorProfile Doctor { get; set; }

        // public Guid InstitutionId { get; set; }
        // public InstitutionProfile Institution { get; set; }

        // public Guid SpecialityId { get; set; }
        // public Speciality Speciality { get; set; }
    }
}