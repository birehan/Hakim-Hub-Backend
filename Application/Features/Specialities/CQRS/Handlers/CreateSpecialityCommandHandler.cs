
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Commands;
using Application.Features.Specialities.DTOs.Validators;
using Application.Responses;
using MediatR;
using Domain;

namespace Application.Features.Specialities.CQRS.Handlers
{
    public class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateSpecialityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<Guid>> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateSpecialityDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SpecialityDto);

            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors[0].ErrorMessage);


            var Speciality = _mapper.Map<Speciality>(request.SpecialityDto);
            await _unitOfWork.SpecialityRepository.Add(Speciality);

            if (await _unitOfWork.Save() > 0)
                return Result<Guid>.Success(Speciality.Id);

            return Result<Guid>.Failure("Creation Failed");

        }
    }
}
