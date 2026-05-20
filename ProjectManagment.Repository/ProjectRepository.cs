using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IRepositories;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Repository.Common;
using ProjectManagment.Repository.Helpers;

namespace ProjectManagment.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IProjectManagmentDbContext context) : base(context)
        {
        }

        public async Task<DataTableResponseVM<Project>> Search(DataTableRequestVM requestVM)
        {
            var search = string.IsNullOrEmpty(requestVM.Search) ? string.Empty : requestVM.Search.ToLower();

            var result = GetAll().OrderByDescending(w => w.Id).Where(w => !w.IsDeleted)
                .Where(w => w.Id.ToString() == search || w.Name.ToLower().Contains(search))
                .Select(w => new Project
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description
                });

            int totalRecords = result.Count();

            result = result.OrderByDynamic(requestVM.OrderColumn, requestVM.OrderDir)
                .Skip(requestVM.PageNumber * requestVM.PageSize)
                .Take(requestVM.PageSize);

            return await result.ToDataTableResult(totalRecords);
        }
    }
}