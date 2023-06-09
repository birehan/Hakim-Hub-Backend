using FluentValidation;

namespace Application.Features.Educations.DTOs.Validators;

public class UpdateEducationDtoValidator : AbstractValidator<UpdateEducationDto>
{
    public UpdateEducationDtoValidator()
    {
        Include(new IEducationDtoValidator());

    }
}
