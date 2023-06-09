using FluentValidation;

namespace Application.Features.Services.DTOs.Validators;

public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceDtoValidator()
        {
            Include(new IServiceDtoValidator());
        }
    }
