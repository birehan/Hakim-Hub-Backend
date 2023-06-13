using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Queries;
using Application.Responses;
using MediatR;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class GetInstitutionProfileListQueryHandler : IRequestHandler<GetInstitutionProfileListQuery, Result<List<InstitutionProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInstitutionProfileListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<InstitutionProfileDto>>> Handle(GetInstitutionProfileListQuery request, CancellationToken cancellationToken)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            var InstitutionProfiles = await _unitOfWork.InstitutionProfileRepository.GetAllPopulated();

            if (InstitutionProfiles == null) return Result<List<InstitutionProfileDto>>.Failure(error: "Item not found.");
=======
            var InstitutionProfiles = await _unitOfWork.InstitutionProfileRepository.GetAll();

            if (InstitutionProfiles == null) return null;
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
            var InstitutionProfiles = await _unitOfWork.InstitutionProfileRepository.GetAllPopulated();

            if (InstitutionProfiles == null) return Result<List<InstitutionProfileDto>>.Failure(error: "Item not found.");
>>>>>>> 95d003c (fix(clean-biruk): clean up)

            return Result<List<InstitutionProfileDto>>.Success(_mapper.Map<List<InstitutionProfileDto>>(InstitutionProfiles));
        }
    }
}
