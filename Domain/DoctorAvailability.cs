using Domain.Common;

namespace Domain
{
    public class DoctorAvailability: BaseDomainEntity
    {
        public string doctorId { get; set; }

        public string institutionId { get; set; }

        public string specialityId {get; set;}
        public DayOfWeek availableDays {get; set;}
        public DateTime startTime {get; set;}
        public DateTime endTime {get; set;}
    }
}