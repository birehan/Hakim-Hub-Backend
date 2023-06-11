using Application.Contracts.Persistence;
using Application.Features.Addresses.CQRS.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.Specialities.CQRS.Handlers
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {

            var Address = await _unitOfWork.AddressRepository.Get(request.Id);

            if (Address is null) return null;

            await _unitOfWork.AddressRepository.Delete(Address);

            if (await _unitOfWork.Save() > 0)
                return Result<Guid>.Success(Address.Id);

            return Result<Guid>.Failure("Delete Failed");

        }
    }
}
