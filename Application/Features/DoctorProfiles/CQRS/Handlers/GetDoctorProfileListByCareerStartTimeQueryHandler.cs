using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using Application.Exceptions;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileListByCareerStartTimeQueryHandler : IRequestHandler<GetDoctorProfileListByCareerStartTimeQuery, Result<List<DoctorProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileListByCareerStartTimeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<DoctorProfileDto>>> Handle(GetDoctorProfileListByCareerStartTimeQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<DoctorProfileDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.GetDoctorProfileByCareerStartTime(request.CareerStartTime);
            if (doctorProfiles is null || doctorProfiles.Count == 0)
            {
                response.IsSuccess = false;
                response.Error = $"{new NotFoundException(nameof(doctorProfiles), request.CareerStartTime)}";
                return response;

            }
            else
            {
                response.IsSuccess = true;
                response.Value = _mapper.Map<List<DoctorProfileDto>>(doctorProfiles);
                return response;
            }
        }
    }
}