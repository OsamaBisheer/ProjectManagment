using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Task;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Domain.Interfaces.IServices
{
    public interface ITaskService
    {
        Task<Tuple<int, ResponseCodeEnum>> Create(TaskCreateVM workflowVM);

        Task<Tuple<int, ResponseCodeEnum>> Update(TaskUpdateVM workflowVM);

        Task<Tuple<DataTableResponseVM<TaskResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM workflowVM);

        Task<Tuple<TaskResultVM, ResponseCodeEnum>> GetById(int id);

        Task<Tuple<List<TaskResultVM>, ResponseCodeEnum>> GetByProjectId(int id);

        Task<Tuple<int, ResponseCodeEnum>> GetCount();

        Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId);
    }
}