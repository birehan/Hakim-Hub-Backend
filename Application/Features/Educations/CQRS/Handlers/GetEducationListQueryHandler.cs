using Application.Contracts.Persistence;
using Application.Features.Educations.CQRS.Queries;
using Application.Features.Educations.DTOs;
using Application.Responses;
using AutoMapper;
using MediatR;
namespace Application.Features.Educations.CQRS.Handlers;

public class GetEducationListQueryHandler : IRequestHandler<GetEducationListQuery, Result<List<EducationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEducationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<EducationDto>>> Handle(GetEducationListQuery request, CancellationToken cancellationToken)
    {
        var education = await _unitOfWork.EducationRepository.GetAll();
        if (education == null)
        {
            return null;
        }
        var educationMapped = _mapper.Map<List<EducationDto>>(education);
        return Result<List<EducationDto>>.Success(educationMapped);
    }
}
