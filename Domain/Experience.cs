using Domain.Common;

namespace Domain;

public class Experience : BaseDomainEntity
{
    public string Position { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string DoctorProfileId { get; set; }
    
    public string InstitutionId { get; set; }
   
}
