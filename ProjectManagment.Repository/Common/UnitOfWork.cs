using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IRepositories;

namespace ProjectManagment.Repository.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public IProjectManagmentDbContext Context { get; }
        public IProjectRepository Projects { get; private set; }
        public ITaskRepository Tasks { get; private set; }

        public UnitOfWork(IProjectManagmentDbContext _context)
        {
            Context = _context;

            Projects = new ProjectRepository(_context);
            Tasks = new TaskRepository(_context);
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public async Task<int> Commit()
        {
            // Save changes with the default options
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}