using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Project;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Domain.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<Tuple<int, ResponseCodeEnum>> Create(ProjectCreateVM workflowVM);

        Task<Tuple<int, ResponseCodeEnum>> Update(ProjectUpdateVM workflowVM);

        Task<Tuple<DataTableResponseVM<ProjectResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM workflowVM);

        Task<Tuple<ProjectResultVM, ResponseCodeEnum>> GetById(int id);

        Task<Tuple<int, ResponseCodeEnum>> GetCount();

        Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId);
    }
}