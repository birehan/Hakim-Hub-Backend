
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Responses;
using MediatR;
using Domain;
using Application.Features.Educations.DTOs;
using Application.Features.Educations.DTOs.Validators;


namespace Application.Features.Educations.CQRS.Handlers;

public class UpdateEducationCommandHandler: IRequestHandler<UpdateEducationCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateEducationDtoValidator();
        var validationResult = await validator.ValidateAsync(request.updateEducationDto);

        if (!validationResult.IsValid)
            return Result<Unit>.Failure(validationResult.Errors[0].ErrorMessage);


        var education = await _unitOfWork.EducationRepository.Get(request.updateEducationDto.Id);
        if (education == null) return null;

        _mapper.Map(request.updateEducationDto, education);
        await _unitOfWork.EducationRepository.Update(education);

        if (await _unitOfWork.Save() > 0)
            return Result<Unit>.Success(Unit.Value);

        return Result<Unit>.Failure("Update Failed");

    }
}
