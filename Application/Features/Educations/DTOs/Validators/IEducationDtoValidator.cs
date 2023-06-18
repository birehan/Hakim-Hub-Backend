using FluentValidation;

namespace Application.Features.Educations.DTOs.Validators;

public class IEducationDtoValidator : AbstractValidator<IEducationDto>
{
    public IEducationDtoValidator()
    {
        
       RuleFor(p => p.DoctorId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");


    }
}
