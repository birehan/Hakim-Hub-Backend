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
    public class GetDoctorProfileListBySpecialityIdQueryHandler : IRequestHandler<GetDoctorProfileListBySpecialityIdQuery, Result<List<DoctorProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileListBySpecialityIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<DoctorProfileDto>>> Handle(GetDoctorProfileListBySpecialityIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<DoctorProfileDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.GetDoctorProfileBySpecialityId(request.SpecialityId);
            if (doctorProfiles is null || doctorProfiles.Count == 0)
            {
                var error = new NotFoundException(nameof(doctorProfiles), request.SpecialityId);
                response.IsSuccess = false;
                response.Error = $"{error}";
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