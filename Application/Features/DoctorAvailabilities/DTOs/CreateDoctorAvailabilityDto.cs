namespace Application.Features.DoctorAvailabilities.DTOs
{
    public class CreateDoctorAvailabilityDto : IDoctorAvailabilityDto
    {
        public DayOfWeek Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool TwentyFourHours { get; set; }
        public DayOfWeek StartDay {get; set;}

        public DayOfWeek EndDay {get; set;}

        public Guid DoctorId { get; set; }
    }
}