using Microsoft.AspNetCore.Identity;

namespace ProjectManagment.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}