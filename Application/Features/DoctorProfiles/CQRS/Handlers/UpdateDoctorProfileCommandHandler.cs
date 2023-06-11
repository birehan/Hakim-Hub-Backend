using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.DoctorProfiles.CQRS.Commands;
using Application.Features.DoctorProfiles.DTOs.Validators;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class UpdateDoctorProfileCommandHandler : IRequestHandler<UpdateDoctorProfileCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDoctorProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> Handle(UpdateDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new Result<Unit>();

            var validator = new UpdateDoctorProfileDtoValidator(_unitOfWork);

            var validationResult = await validator.ValidateAsync(request.updateDoctorProfileDto);
            if (validationResult.IsValid == false)
            {
                response.IsSuccess = false;
                response.Error = $"{validationResult.Errors.Select(e => e.ErrorMessage).ToList()}";
                return response;

            }
            var doctorProfile = await _unitOfWork.DoctorProfileRepository.Get(request.updateDoctorProfileDto.Id);
            if (doctorProfile is null)
            {
                var error = new NotFoundException(nameof(doctorProfile), request.updateDoctorProfileDto.Id);
                response.IsSuccess = false;
                response.Error = $"{error}";
                return response;
            }

            _mapper.Map(doctorProfile, request.updateDoctorProfileDto);
            await _unitOfWork.DoctorProfileRepository.Update(doctorProfile);
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