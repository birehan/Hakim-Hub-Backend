using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs.Validators;
using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.CQRS.Commands;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class UpdateInstitutionProfileCommandHandler : IRequestHandler<UpdateInstitutionProfileCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInstitutionProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateInstitutionProfileCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateInstitutionProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InstitutionProfileDto);

            if (!validationResult.IsValid)
                return Result<Unit>.Failure(validationResult.Errors[0].ErrorMessage);


            var InstitutionProfile = await _unitOfWork.InstitutionProfileRepository.Get(request.InstitutionProfileDto.Id);
            if (InstitutionProfile == null) return null;

            _mapper.Map(request.InstitutionProfileDto, InstitutionProfile);
            await _unitOfWork.InstitutionProfileRepository.Update(InstitutionProfile);

            if (await _unitOfWork.Save() > 0)
                return Result<Unit>.Success(Unit.Value);

            return Result<Unit>.Failure("Update Failed");

        }
    }
}
