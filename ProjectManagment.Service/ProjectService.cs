using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IServices;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Project;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProjectService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<Tuple<DataTableResponseVM<ProjectResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM requestVM)
        {
            var dataTableResponse = await unitOfWork.Projects.Search(requestVM);
            return Tuple.Create(mapper.Map<DataTableResponseVM<Project>, DataTableResponseVM<ProjectResultVM>>(dataTableResponse), ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Create(ProjectCreateVM model)
        {
            var Project = mapper.Map<ProjectCreateVM, Project>(model);

            await unitOfWork.Projects.Add(Project);

            await unitOfWork.Commit();

            return Tuple.Create(Project.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Update(ProjectUpdateVM model)
        {
            var ProjectDB = await unitOfWork.Projects.Get(p => p.Id == model.Id && !p.IsDeleted).AsNoTracking().FirstOrDefaultAsync();

            if (ProjectDB == null || ProjectDB.IsDeleted) return Tuple.Create(0, ResponseCodeEnum.NotFound);

            var Project = mapper.Map<ProjectUpdateVM, Project>(model);

            Project.SetCreated(ProjectDB.CreatedByUserId, ProjectDB.CreatedOn);

            unitOfWork.Projects.Update(Project);

            await unitOfWork.Commit();

            return Tuple.Create(Project.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<ProjectResultVM, ResponseCodeEnum>> GetById(int id)
        {
            var Project = await unitOfWork.Projects.Find(id);

            if (Project == null || Project.IsDeleted) return Tuple.Create(new ProjectResultVM(), ResponseCodeEnum.NotFound);

            var model = mapper.Map<Project, ProjectResultVM>(Project);

            return Tuple.Create(model, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> GetCount()
        {
            var count = await unitOfWork.Projects.Get(p => !p.IsDeleted).CountAsync();

            return Tuple.Create(count, ResponseCodeEnum.Success);
        }

        public async Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId)
        {
            var Project = await unitOfWork.Projects.Get(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            Project.MarkAsDeleted(lastUpdatedByUserId);
            await unitOfWork.Commit();

            return ResponseCodeEnum.Success;
        }
    }
}