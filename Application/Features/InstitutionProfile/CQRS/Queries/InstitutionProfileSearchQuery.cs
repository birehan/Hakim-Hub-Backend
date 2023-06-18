
using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.Specialities.CQRS.Queries
{
    public class InstitutionProfileSearchQuery: IRequest<Result<List<InstitutionProfileDto>>>
    {
        public string? ServiceName { get; set; } = null;
        public int OperationYears { get; set; } = -1;
        public bool OpenStatus { get; set; } = false;
    }
}