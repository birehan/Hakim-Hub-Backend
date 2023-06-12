using AutoMapper;
using Application.Contracts.Persistence;
using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class GetInstitutionProfileByServiceQueryHandler : IRequestHandler<GetInstitutionProfileByServiceQuery, Result<List<InstitutionProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInstitutionProfileByServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<InstitutionProfileDto>>> Handle(GetInstitutionProfileByServiceQuery request, CancellationToken cancellationToken)
        {
            var InstitutionProfile = await _unitOfWork.InstitutionProfileRepository.GetByService(request.ServiceId);
            
<<<<<<< HEAD
            if (InstitutionProfile == null) return Result<List<InstitutionProfileDto>>.Failure(error: "Item not found.");
=======
            if (InstitutionProfile == null) return null;
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

            return Result<List<InstitutionProfileDto>>.Success(_mapper.Map<List<InstitutionProfileDto>>(InstitutionProfile));
        }
    }
}