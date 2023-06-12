using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.DTOs.Validators
{
    public class CreateDoctorProfileDtoValidator : AbstractValidator<CreateDoctorProfileDto>
    {

        public CreateDoctorProfileDtoValidator()
        {
            RuleFor(p => p.FullName)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .NotEmpty()
            .WithMessage("{PropertyName} must be present")
            .MaximumLength(50)
            .WithMessage("{PropertyName} must be less than {PropertyValue}");

            RuleFor(p => p.About)
           .NotNull()
           .WithMessage("{PropertyName} is required")
           .NotEmpty()
           .WithMessage("{PropertyName} must be present")
           .MaximumLength(100)
           .WithMessage("{PropertyName} must be less than {PropertyValue}");

            RuleFor(p => p.Email)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .NotEmpty()
            .WithMessage("{PropertyName} must be present")
            .EmailAddress();

            RuleFor(p => p.CareerStartTime)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .NotEmpty()
            .WithMessage("{PropertyName} must be present")
            .YearMonthDate();


            RuleFor(p => p.Photo)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .NotEmpty()
            .WithMessage("{PropertyName} must be present")
            .IsValidPhotoExtension();

            RuleFor(p => p.Gender)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .NotEmpty()
            .WithMessage("{PropertyName} must be present")
            .Must(gender => gender == GenderType.Male || gender == GenderType.Female)
            .WithMessage("{PropertyName} must be 'male' or 'female'");





        }

    }
}