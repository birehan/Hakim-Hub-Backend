using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using MediatR;
using AutoMapper;
using Application.Features.InstitutionProfiles.DTOs;


namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class FilterDoctorProfilesQueryHandler : IRequestHandler<FilterDoctorProfilesQuery, Result<List<InstitutionDoctorDto>>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public FilterDoctorProfilesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<InstitutionDoctorDto>>> Handle(FilterDoctorProfilesQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<InstitutionDoctorDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.FilterDoctors(request.InstitutionId, request.SpecialityName, request.ExperienceYears, request.EducationName);
            if (doctorProfiles is null)
            {
                response.IsSuccess = false;
                response.Error = "not found";
                return response;
            }
            else
            {
                response.IsSuccess = true;
                response.Value = _mapper.Map<List<InstitutionDoctorDto>>(doctorProfiles);
                return response;
            }
        }
    }

}
