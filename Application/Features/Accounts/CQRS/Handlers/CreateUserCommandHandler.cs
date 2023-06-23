using MediatR;
using Application.Responses;
using Domain;
using Application.Features.Accounts.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Application.Features.Accounts.DTOs.Validators;

namespace Application.Features.Accounts.CQRS.Handler
{

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateUserDtoValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request.CreateUserDto);

            if (!validationResult.IsValid)
                return Result<string>.Failure(validationResult.Errors[0].ErrorMessage);


            var user = new AppUser
            {
                Email = request.CreateUserDto.Email,
                UserName = request.CreateUserDto.UserName,
                UserRole = AppUser.Role.User
            };

            var result = await _userManager.CreateAsync(user, request.CreateUserDto.Password);
            if (result.Succeeded)
            {
                return Result<string>.Success("Create User Success.");
            }

            return Result<string>.Failure(result!?.Errors.ToList()[0].ToString());
        }
    }
}