using Application.Features.InstitutionProfiles.DTOs.Validators;
using Application.Features.InstitutionProfiles.DTOs;
using FluentValidation;

namespace Application.Features.Specialities.DTOs.Validators

{
    public class UpdateInstitutionProfileDtoValidator : AbstractValidator<UpdateInstitutionProfileDto>
    {
        public UpdateInstitutionProfileDtoValidator()
        {
            Include(new IInstitutionProfileDtoValidator());

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");

        }
    }
}