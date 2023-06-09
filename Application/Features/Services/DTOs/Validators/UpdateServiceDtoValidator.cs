using FluentValidation;

namespace Application.Features.Services.DTOs.Validators;

public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            Include(new IServiceDtoValidator());
            
            RuleFor(dto => dto.Id).NotNull().WithMessage("{PropertyName} must be present");

        }
    }
