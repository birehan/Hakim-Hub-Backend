
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs.Validators;
using Application.Responses;
using MediatR;
using Domain;
using Application.Features.InstitutionProfiles.CQRS.Commands;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class CreateInstitutionProfileCommandHandler : IRequestHandler<CreateInstitutionProfileCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateInstitutionProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<Guid>> Handle(CreateInstitutionProfileCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateInstitutionProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InstitutionProfileDto);

            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors[0].ErrorMessage);


            var InstitutionProfile = _mapper.Map<InstitutionProfile>(request.InstitutionProfileDto);
            await _unitOfWork.InstitutionProfileRepository.Add(InstitutionProfile);

            if (await _unitOfWork.Save() > 0)
                return Result<Guid>.Success(InstitutionProfile.Id);

            return Result<Guid>.Failure("Creation Failed");

        }
    }
}
