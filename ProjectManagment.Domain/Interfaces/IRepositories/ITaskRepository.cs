using ProjectManagment.Domain.ViewModels.Common;

namespace ProjectManagment.Domain.Interfaces.IRepositories
{
    public interface ITaskRepository : IGenericRepository<Entities.Task>
    {
        Task<DataTableResponseVM<Entities.Task>> Search(DataTableRequestVM workflowVM);
    }
}