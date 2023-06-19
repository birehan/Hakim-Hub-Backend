using AutoMapper;
using Application.Contracts.Persistence;
using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class GetInstitutionProfileDetailQueryHandler : IRequestHandler<GetInstitutionProfileDetailQuery, Result<InstitutionProfileDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInstitutionProfileDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<InstitutionProfileDetailDto>> Handle(GetInstitutionProfileDetailQuery request, CancellationToken cancellationToken)
        {
            var InstitutionProfile = await _unitOfWork.InstitutionProfileRepository.GetPopulatedInstitution(request.Id);

            if (InstitutionProfile == null) return Result<InstitutionProfileDetailDto>.Failure(error: "Item not found.");

            return Result<InstitutionProfileDetailDto>.Success(_mapper.Map<InstitutionProfileDetailDto>(InstitutionProfile));
        }
    }
}