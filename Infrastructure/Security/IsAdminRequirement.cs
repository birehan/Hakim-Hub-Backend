using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using static Domain.AppUser;

namespace Infrastructure.Security
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {

    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly UserManager<AppUser> _userManager;

        public IsAdminRequirementHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var Email = context.User.FindFirstValue(ClaimTypes.Email);
            if (Email == null)
            {
                context.Fail(new AuthorizationFailureReason(this, "no user"));
                return;
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == Email);

            if (user != null && user.UserRole == Role.Admin)
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail(new AuthorizationFailureReason(this, "i dont knowr"));
        }
    }
}
