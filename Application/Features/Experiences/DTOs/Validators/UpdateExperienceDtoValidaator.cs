using FluentValidation;

namespace Application.Features.Experiences.DTOs.Validators;

public class UpdateExperienceDtoValidator : AbstractValidator<UpdateExperienceDto>
    {
        public UpdateExperienceDtoValidator()
        {
            Include(new IExperienceDtoValidator());
            
            RuleFor(dto => dto.Id).NotNull().WithMessage("{PropertyName} must be present");

        }
    }
