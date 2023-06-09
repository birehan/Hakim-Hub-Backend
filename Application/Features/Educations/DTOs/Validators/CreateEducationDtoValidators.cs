using FluentValidation;

namespace Application.Features.Educations.DTOs.Validators;

public class CreateEducationDtoValidators : AbstractValidator<CreateEducationDto>
{
    public CreateEducationDtoValidators()
    {
        Include(new IEducationDtoValidator());
    }

}
