using Application.Features.Accounts.DTOs;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Accounts.CQRS.Commands
{  
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ResetPasswordDto.UserId);

            if (user == null)
            {
                return Result<string>.Failure("User not found!");
            }

            var resetResult = await _userManager.ResetPasswordAsync(
                user,
                request.ResetPasswordDto.Token,
                request.ResetPasswordDto.NewPassword);

            if (resetResult.Succeeded)
            {
                return Result<string>.Success("Password reset successfully!");
            }

            var errors = resetResult.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure("Password reset failed: " + string.Join(", ", errors));
        }
    }
}
