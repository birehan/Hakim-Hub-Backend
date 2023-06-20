using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Responses;
using Application.Features.Accounts.CQRS.Commands;


namespace Application.Features.Accounts.CQRS.Handler
{

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IUserAccessor _userAccessor;


        public UpdatePasswordCommandHandler(UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<Result<string>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken: cancellationToken);

            if (user == null)
            {
                return Result<string>.Failure("Unauthorized");
            }

            // Verify if the old password matches the user's current password
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.UpdatePasswordDto.OldPassword);
            if (!passwordValid)
            {
                return Result<string>.Failure("Invalid old password");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.UpdatePasswordDto.OldPassword, request.UpdatePasswordDto.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Result<string>.Success("Password updated successfully.");
            }

            var errorMessage = string.Join(", ", changePasswordResult.Errors.Select(e => e.Description));
            return Result<string>.Failure(errorMessage);


        }
    }
}