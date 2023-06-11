using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using Application.Exceptions;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileQueryHandler : IRequestHandler<GetDoctorProfileQuery, Result<DoctorProfileDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<DoctorProfileDto>> Handle(GetDoctorProfileQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<DoctorProfileDto>();
            var doctorProfile = await _unitOfWork.DoctorProfileRepository.Get(request.Id);
            if (doctorProfile is null)
            {
                var error = new NotFoundException(nameof(doctorProfile), request.Id);
                response.IsSuccess = false;
                response.Error = $"{error}";
                return response;


            }
            else
            {
                response.IsSuccess = true;
                response.Value = _mapper.Map<DoctorProfileDto>(doctorProfile);
                return response;
            }


        }
    }
}