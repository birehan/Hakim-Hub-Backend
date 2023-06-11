using Application.Contracts.Persistence;
using Application.Features.DoctorProfiles.CQRS.Commands;
using Application.Features.DoctorProfiles.DTOs.Validators;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class CreateDoctorProfileCommandHandler : IRequestHandler<CreateDoctorProfileCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDoctorProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Guid>> Handle(CreateDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new Result<Guid>();
            var validator = new CreateDoctorProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDoctorProfileDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Error = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
                return response;
            }

            var doctorProfile = _mapper.Map<DoctorProfile>(request.CreateDoctorProfileDto);
            await _unitOfWork.DoctorProfileRepository.Add(doctorProfile);

            if (await _unitOfWork.Save() == 0)
            {
                response.IsSuccess = false;
                response.Error = "Server error";
                return response;
            }
            else
            {
                response.IsSuccess = true;
                response.Value = doctorProfile.Id; // Assuming doctorProfile.Id is the identifier
                return response;
            }
        }
    }
}