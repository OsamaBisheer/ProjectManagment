using ProjectManagment.Domain.Interfaces.IRepositories;

namespace ProjectManagment.Domain.Interfaces.ICore
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ITaskRepository Tasks { get; }

        IProjectManagmentDbContext Context { get; }

        Task<int> Commit();
    }
}