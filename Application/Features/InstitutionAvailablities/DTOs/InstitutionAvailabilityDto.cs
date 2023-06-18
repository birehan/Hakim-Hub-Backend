using Application.Features.Common;

namespace Application.Features.InstitutionAvailabilities.DTOs
{
    public class InstitutionAvailabilityDto : BaseDto, IInstitutionAvailabilityDto
    {
        public DayOfWeek StartDay { get; set; }
        public DayOfWeek EndDay { get; set; }
        public string Opening { get; set; }
        public string Closing { get; set; }
        public bool TwentyFourHours { get; set; }
        public Guid InstitutionId { get; set; }
       

    }
}