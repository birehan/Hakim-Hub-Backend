using Domain.Common;

namespace Domain
{
    public class InstitutionAvailability: BaseDomainEntity
    {
        public string institutionId { get; set; }
        public DayOfWeek availableDays {get; set;}
        public DateTime startTime {get; set;}
        public DateTime endTime {get; set;}
    }
}