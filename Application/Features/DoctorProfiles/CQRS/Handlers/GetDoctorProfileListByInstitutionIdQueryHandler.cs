using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileListByInstitutionIdQueryHandler : IRequestHandler<GetDoctorProfileListByInstitutionIdQuery, Result<List<DoctorProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileListByInstitutionIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<List<DoctorProfileDto>>> Handle(GetDoctorProfileListByInstitutionIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<DoctorProfileDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.GetDoctorProfileByInstitutionId(request.InstitutionId);
            if (doctorProfiles is null || doctorProfiles.Count == 0)
            {
                response.IsSuccess = false;
                response.Error = $"{new NotFoundException(nameof(doctorProfiles), request.InstitutionId)}";
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