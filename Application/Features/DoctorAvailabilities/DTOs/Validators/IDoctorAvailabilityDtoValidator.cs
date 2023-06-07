using FluentValidation;

namespace Application.Features.DoctorAvailabilities.DTOs.Validators
{
    public class IDoctorAvailabilityDtoValidator : AbstractValidator<IDoctorAvailabilityDto>
    {
        public IDoctorAvailabilityDtoValidator()
        {
            RuleFor(p => p.doctorId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.institutionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
                
            RuleFor(p => p.specialityId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");



        }

    }
}