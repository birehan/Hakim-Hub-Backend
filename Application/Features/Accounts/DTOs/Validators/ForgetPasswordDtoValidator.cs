using FluentValidation;
using System;
namespace Application.Features.Accounts.DTOs.Validator
{
public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordDtoValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
        }
    }
}