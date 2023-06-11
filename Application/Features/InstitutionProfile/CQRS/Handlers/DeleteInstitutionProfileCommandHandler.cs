using Application.Contracts.Persistence;
using Application.Features.InstitutionProfiles.CQRS.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class DeleteInstitutionProfileCommandHandler : IRequestHandler<DeleteInstitutionProfileCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInstitutionProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteInstitutionProfileCommand request, CancellationToken cancellationToken)
        {

            var InstitutionProfile = await _unitOfWork.InstitutionProfileRepository.Get(request.Id);

<<<<<<< HEAD
            if (InstitutionProfile is null) return Result<Guid>.Failure("Delete Failed");
=======
            if (InstitutionProfile is null) return null;
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)

            await _unitOfWork.InstitutionProfileRepository.Delete(InstitutionProfile);

            if (await _unitOfWork.Save() > 0)
                return Result<Guid>.Success(InstitutionProfile.Id);

            return Result<Guid>.Failure("Delete Failed");

        }
    }
}
