using FluentValidation;
using System;
namespace Application.Features.Accounts.DTOs.Validator
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(dto => dto.UserId).NotEmpty();
            RuleFor(dto => dto.Token).NotEmpty();
            RuleFor(dto => dto.NewPassword).NotEmpty().MinimumLength(8);
        }
    }
}