
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Responses;
using MediatR;
using Domain;
using Application.Features.Educations.DTOs;
using Application.Features.Educations.DTOs.Validators;

namespace Application.Features.Educations.CQRS.Handlers;

public class CreateEducationCommandHandler: IRequestHandler<CreateEducationCommand, Result<CreateEducationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateEducationDto>> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateEducationDtoValidators();
        var validationResult = await validator.ValidateAsync(request.createEducationDto);

        if (!validationResult.IsValid)
            return Result<CreateEducationDto>.Failure(validationResult.Errors[0].ErrorMessage);


        var education = _mapper.Map<Education>(request.createEducationDto);
        var edu = await _unitOfWork.EducationRepository.Add(education);


        if (await _unitOfWork.Save() > 0)
            return Result<CreateEducationDto>.Success(_mapper.Map<CreateEducationDto>(edu));

        return Result<CreateEducationDto>.Failure("Creation Failed");
    }

}
