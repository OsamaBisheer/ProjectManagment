using Microsoft.AspNetCore.Mvc;
using ProjectManagment.API.Controllers;
using ProjectManagment.Domain.Interfaces.IServices;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Project;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : CommonControllerBase
    {
        private readonly IProjectService ProjectService;

        public ProjectController(IProjectService _ProjectService)
        {
            ProjectService = _ProjectService;
        }

        [HttpGet, Route("by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            (ProjectResultVM model, ResponseCodeEnum responseCode) = await ProjectService.GetById(id);

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
            (int count, ResponseCodeEnum responseCode) = await ProjectService.GetCount();

            return GetActionResult(new ResponseModel
            {
                Result = count,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult> GetProjectsForPagination([FromQuery] DataTableRequestVM requestVM)
        {
            (DataTableResponseVM<ProjectResultVM> dataTableResponseVM, ResponseCodeEnum responseCode) = await ProjectService.Search(requestVM);

            return GetActionResult(new ResponseModel
            {
                Result = dataTableResponseVM,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(ProjectCreateVM createVM)
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
            (int id, ResponseCodeEnum responseCode) = await ProjectService.Create(createVM);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPut, Route("update")]
        public async Task<ActionResult> Update(ProjectUpdateVM updateVM)
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

            (int id, ResponseCodeEnum responseCode) = await ProjectService.Update(updateVM);

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
            ResponseCodeEnum responseCode = await ProjectService.Delete(id, currentUserId);

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