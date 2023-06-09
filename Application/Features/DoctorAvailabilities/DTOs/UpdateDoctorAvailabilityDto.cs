using Application.Features.Common;

namespace Application.Features.DoctorAvailabilities.DTOs
{
    public class UpdateDoctorAvailabilityDto : BaseDto, IDoctorAvailabilityDto
    {
<<<<<<< HEAD
        public DayOfWeek Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
=======
>>>>>>> 163417f (Feat(backend-HakimHub): Add controller)
        public bool TwentyFourHours { get; set; }
        public DayOfWeek StartDay {get; set;}

        public DayOfWeek EndDay {get; set;}
<<<<<<< HEAD
=======

        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}
        public Guid InstitutionId { get; set; }
>>>>>>> 163417f (Feat(backend-HakimHub): Add controller)

        public Guid DoctorId { get; set; }
    }
}