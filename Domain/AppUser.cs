using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public enum Role
        {
            Admin,
            User
        }

        public Role UserRole { get; set; }
    }
}