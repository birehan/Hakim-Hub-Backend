
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
        education.Id = Guid.NewGuid();
        var edu = await _unitOfWork.EducationRepository.Add(education);


        var response = new Result<CreateEducationDto>();
        if (await _unitOfWork.Save() > 0){
            response.Value = _mapper.Map<CreateEducationDto>(edu);
            response.IsSuccess = true;
            response.Error = "Create Education Successful.";
            
        }else{
            response.Value = null;
            response.IsSuccess = false;
            response.Error = "Create Education Failed.";
        }
        return response;
    }
}
