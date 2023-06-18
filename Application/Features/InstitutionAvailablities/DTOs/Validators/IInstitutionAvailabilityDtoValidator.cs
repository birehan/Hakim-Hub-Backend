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
                
            RuleFor(p => p.Opening)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(p => p.Closing)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();



        }

    }
}