using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Queries;
using Application.Features.Specialities.DTOs;
using MediatR;
using Application.Responses;
using Application.Features.Addresses.CQRS.Queries;
using Application.Features.Addresses.DTOs;

namespace Application.Features.Addresses.CQRS.Handlers
{
    public class GetAddressDetailQueryHandler : IRequestHandler<GetAddressDetailQuery, Result<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAddressDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddressDto>> Handle(GetAddressDetailQuery request, CancellationToken cancellationToken)
        {
<<<<<<< HEAD
            var Address = await _unitOfWork.AddressRepository.GetPopulated(request.Id);

            if (Address == null) return Result<AddressDto>.Failure(error: "Item not found.");
;
=======
            var Address = await _unitOfWork.AddressRepository.Get(request.Id);

<<<<<<< HEAD
            if (Address == null) return null;
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
            if (Address == null) return Result<AddressDto>.Failure(error: "Item not found.");
;
>>>>>>> 95d003c (fix(clean-biruk): clean up)

            return Result<AddressDto>.Success(_mapper.Map<AddressDto>(Address));
        }
    }
}