using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Contracts.Persistence;
using Application.Responses;
using AutoMapper;
using MediatR;
using Application.Exceptions;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileListByGenderQueryHandler : IRequestHandler<GetDoctorProfileListByGenderQuery, Result<List<DoctorProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileListByGenderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<DoctorProfileDto>>> Handle(GetDoctorProfileListByGenderQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<DoctorProfileDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.GetDoctorProfileByGender(request.Gender);
            if (doctorProfiles is null || doctorProfiles.Count == 0)
            {
                response.IsSuccess = false;
                response.Error = $"{new NotFoundException(nameof(doctorProfiles), request.Gender)}";
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