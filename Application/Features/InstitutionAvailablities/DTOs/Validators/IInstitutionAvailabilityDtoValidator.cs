using FluentValidation;

namespace Application.Features.InstitutionAvailabilities.DTOs.Validators
{
    public class IInstitutionAvailabilityDtoValidator : AbstractValidator<IInstitutionAvailabilityDto>
    {
        public IInstitutionAvailabilityDtoValidator()
        {
    

            RuleFor(p => p.InstitutionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
                
            RuleFor(p => p.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(p => p.EndTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();



        }

    }
}