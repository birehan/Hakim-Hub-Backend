using Domain.Common;
namespace Domain
{
    public class InstitutionAvailability : BaseDomainEntity
    {
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string Opening { get; set; }
        public string Closing { get; set; }
        public bool TwentyFourHours { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionProfile Institution { get; set; }
    }
}