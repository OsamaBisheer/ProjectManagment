using Microsoft.AspNetCore.Identity;

namespace ProjectManagment.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}