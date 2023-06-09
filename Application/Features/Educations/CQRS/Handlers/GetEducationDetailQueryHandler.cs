using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS.Queries;
using Application.Features.Educations.DTOs;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Educations.CQRS.Handlers;

public class GetEducationDetailQueryHandler : IRequestHandler<GetEducationDetailQuery, Result<EducationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEducationDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<EducationDto>> Handle(GetEducationDetailQuery request, CancellationToken cancellationToken)
    {
        var education = await _unitOfWork.EducationRepository.Get(request.Id);
        if (education == null){
            return null;
        }
        var educationMapped = _mapper.Map<EducationDto>(education);
        return Result<EducationDto>.Success(educationMapped);
    }
}
