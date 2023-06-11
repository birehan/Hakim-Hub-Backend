
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.DoctorProfiles.CQRS.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class DeleteDoctorProfileCommandHandler : IRequestHandler<DeleteDoctorProfileCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDoctorProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new Result<Unit>();
            var doctorProfile = await _unitOfWork.DoctorProfileRepository.Get(request.Id);
            if (doctorProfile is null)
            {

                response.IsSuccess = false;
                response.Error = $"{new NotFoundException(nameof(doctorProfile), request.Id)}";
                return response;

            }
            await _unitOfWork.DoctorProfileRepository.Delete(doctorProfile);
            if (await _unitOfWork.Save() == 0)
            {
                response.IsSuccess = false;
                response.Error = "server error";
                return response;
            }
            else
            {
                response.IsSuccess = true;
                return response;
            }

        }

    }
}