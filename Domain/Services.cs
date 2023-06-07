using Domain.Common;

namespace Domain;

public class Services : BaseDomainEntity
{
    public string ServiceName { get; set; } 
    public string ServiceDescription { get; set; }
    public string InstitutionId { get; set; }
}
