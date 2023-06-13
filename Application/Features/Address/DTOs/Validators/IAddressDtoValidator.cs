using FluentValidation;

namespace Application.Features.Addresses.DTOs.Validators
{
    public class IAddressDtoValidator : AbstractValidator<IAddressDto>
    {
        public IAddressDtoValidator()
        {
            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Region)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Zone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Woreda)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SubCity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
            
            RuleFor(p => p.Summary)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
<<<<<<< HEAD

            RuleFor(p => p.Longitude)
                .NotNull();

            RuleFor(p => p.Latitude)
=======
=======
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)

            RuleFor(p => p.Longitude)
                .NotNull();

            RuleFor(p => p.Latitude)
<<<<<<< HEAD
                .NotEmpty().WithMessage("{PropertyName} is required.")
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
>>>>>>> 95d003c (fix(clean-biruk): clean up)
                .NotNull();   
        }

    }
}