namespace Application.Features.InstitutionAvailabilities.DTOs
{
    public interface IInstitutionAvailabilityDto
    {
        public DayOfWeek StartDay { get; set; }
        public DayOfWeek EndDay { get; set; }
        public string Opening { get; set; }
        public string Closing { get; set; }
        public bool TwentyFourHours { get; set; }
        public DayOfWeek StartDay {get; set;}

        public DayOfWeek EndDay {get; set;}

        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}
        public Guid InstitutionId { get; set; }
    }
}