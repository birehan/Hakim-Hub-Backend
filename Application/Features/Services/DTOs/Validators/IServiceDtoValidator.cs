using FluentValidation;

namespace Application.Features.Services.DTOs.Validators;
public class IServiceDtoValidator : AbstractValidator<IServiceDto>
{
    public IServiceDtoValidator()
    {
        RuleFor(dto => dto.ServiceName)
            .NotEmpty().WithMessage("Service name is required.")
            .MaximumLength(100).WithMessage("Service name must not exceed 100 characters.");

        RuleFor(dto => dto.ServiceDescription)
            .NotEmpty().WithMessage("Service description is required.")
            .MaximumLength(500).WithMessage("Service description must not exceed 500 characters.");
    }
}
