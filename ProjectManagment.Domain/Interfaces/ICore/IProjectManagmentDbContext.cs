using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Domain.Interfaces.ICore
{
    public interface IProjectManagmentDbContext : IDisposable
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Entities.Task> Tasks { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}