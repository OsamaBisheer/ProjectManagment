using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IRepositories;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Repository.Common;
using ProjectManagment.Repository.Helpers;

namespace ProjectManagment.Repository
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(IProjectManagmentDbContext context) : base(context)
        {
        }

        public async Task<DataTableResponseVM<Domain.Entities.Task>> Search(DataTableRequestVM requestVM)
        {
            var search = string.IsNullOrEmpty(requestVM.Search) ? string.Empty : requestVM.Search.ToLower();

            var result = GetAll().OrderByDescending(w => w.Id).Where(w => !w.IsDeleted)
                .Where(w => w.Id.ToString() == search || w.Title.ToLower().Contains(search))
                .Select(w => new Domain.Entities.Task
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    Status = w.Status
                });

            int totalRecords = result.Count();

            result = result.OrderByDynamic(requestVM.OrderColumn, requestVM.OrderDir)
                .Skip(requestVM.PageNumber * requestVM.PageSize)
                .Take(requestVM.PageSize);

            return await result.ToDataTableResult(totalRecords);
        }
    }
}