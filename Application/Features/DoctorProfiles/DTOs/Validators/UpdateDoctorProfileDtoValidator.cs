using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using FluentValidation;
using static Domain.DoctorProfile;

namespace Application.Features.DoctorProfiles.DTOs.Validators
{
    public class UpdateDoctorProfileDtoValidator : AbstractValidator<UpdateDoctorProfileDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDoctorProfileDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                var exists = await _unitOfWork.DoctorProfileRepository.Exists(id);
                return exists;
            })
            .WithMessage("{PropertyName} must exist");

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

            RuleFor(p => p.Gender)
           .NotNull()
           .WithMessage("{PropertyName} is required")
           .NotEmpty()
           .WithMessage("{PropertyName} must be present")
           .Must(gender => gender == GenderType.Male || gender == GenderType.Female)
           .WithMessage("{PropertyName} must be 'male' or 'female'");

            //    RuleFor(p=>p.PhotoId)
            //    .MustAsync(async (phototId,CancellationToken)=>{
            //     var exists = await _unitOfWork.PhotoRepository.Exists(phototId);
            //     return exists;
            //    }).WithMessage("{PropertyName} must exist");



        }

    }
}