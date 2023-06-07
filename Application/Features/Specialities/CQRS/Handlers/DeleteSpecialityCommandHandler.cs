using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.Specialities.CQRS.Handlers
{
    public class DeleteSpecialityCommandHandler : IRequestHandler<DeleteSpecialityCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSpecialityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
        {

            var Speciality = await _unitOfWork.SpecialityRepository.Get(request.Id);

            if (Speciality is null) return null;

            await _unitOfWork.SpecialityRepository.Delete(Speciality);

            if (await _unitOfWork.Save() > 0)
                return Result<Guid>.Success(Speciality.Id);

            return Result<Guid>.Failure("Delete Failed");

        }
    }
}
