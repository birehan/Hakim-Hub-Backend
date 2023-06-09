using FluentValidation;

namespace Application.Features.Experiences.DTOs.Validators;

public class CreateExperienceDtoValidator : AbstractValidator<CreateExperienceDto>
    {
        public CreateExperienceDtoValidator()
        {
            Include(new IExperienceDtoValidator());
        }
    }
