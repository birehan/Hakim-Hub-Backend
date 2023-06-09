using FluentValidation;

namespace Application.Features.Educations.DTOs.Validators;

public class IEducationDtoValidator : AbstractValidator<IEducationDto>
{
    public IEducationDtoValidator()
    {
        RuleFor(p => p.InstitutionName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        RuleFor(p => p.FieldOfStudy)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(300).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        RuleFor(p => p.DoctorId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");


    }
}
