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
        var doctorProfile = await _unitOfWork.DoctorProfileRepository.GetDoctorProfile(request.Id);

        if (doctorProfile == null)
        {
            return Result<DoctorProfileDto>.Failure("${new NotFoundException(nameof(doctorProfile), request.Id}");
        }

        var doctorProfileDto = _mapper.Map<DoctorProfileDto>(doctorProfile);
        return Result<DoctorProfileDto>.Success(doctorProfileDto);
    }
}

}