using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Entities.Identity;
using ProjectManagment.Domain.Interfaces.ICore;

namespace ProjectManagment.Persistence
{
    public class ProjectManagmentDbContext : IdentityDbContext<User, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IProjectManagmentDbContext
    {
        public ProjectManagmentDbContext(DbContextOptions<ProjectManagmentDbContext> options) : base(options)
        { }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
    }
}