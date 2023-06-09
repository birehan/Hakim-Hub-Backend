using Application.Features.Common;

namespace Application.Features.InstitutionAvailabilities.DTOs
{
    public class UpdateInstitutionAvailabilityDto : BaseDto, IInstitutionAvailabilityDto
    {
        public bool TwentyFourHours { get; set; }
        public DayOfWeek StartDay {get; set;}

        public DayOfWeek EndDay {get; set;}

        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}
        public Guid InstitutionId { get; set; }

    }
}