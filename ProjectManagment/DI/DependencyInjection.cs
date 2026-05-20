using ProjectManagment.API.Providers;
using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IRepositories;
using ProjectManagment.Persistence;
using ProjectManagment.Repository.Common;
using ProjectManagment.Service;
using ProjectManagment.Domain.Interfaces.IServices;
using ProjectManagment.API.JWT;
using ProjectManagment.Repository;

namespace ProjectManagment.API.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIs(this IServiceCollection services)
        {
            services.AddScoped<IProjectManagmentDbContext, ProjectManagmentDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityProvider, IdentityProvider>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddHttpClient<ProjectService>();

            services.AddScoped<RevokableJwtSecurityTokenHandler>();
            services.AddScoped<JwtHandlerEvents>();

            return services;
        }
    }
}