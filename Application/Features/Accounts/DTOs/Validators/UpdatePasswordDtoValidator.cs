using FluentValidation;
using System;
namespace Application.Features.Accounts.DTOs.Validator
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(dto => dto.OldPassword).NotEmpty();
            RuleFor(dto => dto.NewPassword).NotEmpty().MinimumLength(8);
        }
    }
}