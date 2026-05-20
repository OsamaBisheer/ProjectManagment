using Microsoft.AspNetCore.Mvc;
using ProjectManagment.API.Controllers;
using ProjectManagment.Domain.Interfaces.IServices;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Task;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : CommonControllerBase
    {
        private readonly ITaskService TaskService;

        public TaskController(ITaskService _TaskService)
        {
            TaskService = _TaskService;
        }

        [HttpGet, Route("by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            (TaskResultVM model, ResponseCodeEnum responseCode) = await TaskService.GetById(id);

            return GetActionResult(new ResponseModel
            {
                Result = model,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("by-project-id/{id}")]
        public async Task<ActionResult> GetByProjectId(int id)
        {
            (List<TaskResultVM> model, ResponseCodeEnum responseCode) = await TaskService.GetByProjectId(id);

            return GetActionResult(new ResponseModel
            {
                Result = model,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("count")]
        public async Task<ActionResult> GetCount()
        {
            (int count, ResponseCodeEnum responseCode) = await TaskService.GetCount();

            return GetActionResult(new ResponseModel
            {
                Result = count,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult> GetTasksForPagination([FromQuery] DataTableRequestVM requestVM)
        {
            (DataTableResponseVM<TaskResultVM> dataTableResponseVM, ResponseCodeEnum responseCode) = await TaskService.Search(requestVM);

            return GetActionResult(new ResponseModel
            {
                Result = dataTableResponseVM,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(TaskCreateVM createVM)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            createVM.CreatedByUserId = GetCurrentUserId();
            (int id, ResponseCodeEnum responseCode) = await TaskService.Create(createVM);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPut, Route("update")]
        public async Task<ActionResult> Update(TaskUpdateVM updateVM)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            (int id, ResponseCodeEnum responseCode) = await TaskService.Update(updateVM);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpDelete, Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            var currentUserId = GetCurrentUserId();
            ResponseCodeEnum responseCode = await TaskService.Delete(id, currentUserId);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }
    }
}