using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.DoctorProfiles.CQRS.Queris;
using Application.Features.DoctorProfiles.DTOs;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorProfiles.CQRS.Handlers
{
    public class GetDoctorProfileByEducationIdQueryHandler: IRequestHandler<GetDoctorProfileByEducationIdQuery, Result<List<DoctorProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorProfileByEducationIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<DoctorProfileDto>>> Handle(GetDoctorProfileByEducationIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Result<List<DoctorProfileDto>>();
            var doctorProfiles = await _unitOfWork.DoctorProfileRepository.GetDoctorProfileByEducationId(request.EducationId);
            if (doctorProfiles is null || doctorProfiles.Count == 0)
            {
                var error = new NotFoundException(nameof(doctorProfiles), request.EducationId);
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
    