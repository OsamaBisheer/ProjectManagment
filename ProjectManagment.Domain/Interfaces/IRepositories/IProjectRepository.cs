using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.ViewModels.Common;

namespace ProjectManagment.Domain.Interfaces.IRepositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<DataTableResponseVM<Project>> Search(DataTableRequestVM workflowVM);
    }
}