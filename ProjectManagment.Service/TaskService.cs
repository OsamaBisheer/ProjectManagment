using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.Interfaces.IServices;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Task;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Service
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<Tuple<DataTableResponseVM<TaskResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM requestVM)
        {
            var dataTableResponse = await unitOfWork.Tasks.Search(requestVM);
            return Tuple.Create(mapper.Map<DataTableResponseVM<Domain.Entities.Task>, DataTableResponseVM<TaskResultVM>>(dataTableResponse), ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Create(TaskCreateVM model)
        {
            var Task = mapper.Map<TaskCreateVM, Domain.Entities.Task>(model);

            await unitOfWork.Tasks.Add(Task);

            await unitOfWork.Commit();

            return Tuple.Create(Task.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Update(TaskUpdateVM model)
        {
            var TaskDB = await unitOfWork.Tasks.Get(p => p.Id == model.Id && !p.IsDeleted).AsNoTracking().FirstOrDefaultAsync();

            if (TaskDB == null || TaskDB.IsDeleted) return Tuple.Create(0, ResponseCodeEnum.NotFound);

            var Task = mapper.Map<TaskUpdateVM, Domain.Entities.Task>(model);

            Task.SetCreated(TaskDB.CreatedByUserId, TaskDB.CreatedOn);

            unitOfWork.Tasks.Update(Task);

            await unitOfWork.Commit();

            return Tuple.Create(Task.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<TaskResultVM, ResponseCodeEnum>> GetById(int id)
        {
            var Task = await unitOfWork.Tasks.Find(id);

            if (Task == null || Task.IsDeleted) return Tuple.Create(new TaskResultVM(), ResponseCodeEnum.NotFound);

            var model = mapper.Map<Domain.Entities.Task, TaskResultVM>(Task);

            return Tuple.Create(model, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<List<TaskResultVM>, ResponseCodeEnum>> GetByProjectId(int id)
        {
            var Tasks = await unitOfWork.Tasks.Get(t => t.ProjectId == id).Select(t => mapper.Map<Domain.Entities.Task, TaskResultVM>(t)).ToListAsync();

            if (Tasks == null || Tasks.Count == 0) return Tuple.Create(new List<TaskResultVM>(), ResponseCodeEnum.NotFound);

            return Tuple.Create(Tasks, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> GetCount()
        {
            var count = await unitOfWork.Tasks.Get(p => !p.IsDeleted).CountAsync();

            return Tuple.Create(count, ResponseCodeEnum.Success);
        }

        public async Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId)
        {
            var Task = await unitOfWork.Tasks.Get(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            Task.MarkAsDeleted(lastUpdatedByUserId);
            await unitOfWork.Commit();

            return ResponseCodeEnum.Success;
        }
    }
}