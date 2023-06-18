using AutoMapper;
using Application.Contracts.Persistence;
using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class GetInstitutionProfileDetailQueryHandler : IRequestHandler<GetInstitutionProfileDetailQuery, Result<InstitutionProfileDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInstitutionProfileDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<InstitutionProfileDto>> Handle(GetInstitutionProfileDetailQuery request, CancellationToken cancellationToken)
        {
            var InstitutionProfile = await _unitOfWork.InstitutionProfileRepository.GetPopulatedInstitution(request.Id);

            if (InstitutionProfile == null) return Result<InstitutionProfileDto>.Failure(error: "Item not found.");

            return Result<InstitutionProfileDto>.Success(_mapper.Map<InstitutionProfileDto>(InstitutionProfile));
        }
    }
}