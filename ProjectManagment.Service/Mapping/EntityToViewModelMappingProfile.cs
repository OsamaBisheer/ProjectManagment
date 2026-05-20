using AutoMapper;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.Project;
using ProjectManagment.Domain.ViewModels.Task;

namespace ProjectManagment.Service.Mappings
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<Project, LookupVM>();
            CreateMap<Project, ProjectResultVM>();
            CreateMap<DataTableResponseVM<Project>, DataTableResponseVM<ProjectResultVM>>();

            CreateMap<Domain.Entities.Task, LookupVM>();
            CreateMap<Domain.Entities.Task, TaskResultVM>();
            CreateMap<DataTableResponseVM<Domain.Entities.Task>, DataTableResponseVM<TaskResultVM>>();
            // CreateMap<List<Domain.Entities.Task>, List<TaskResultVM>>();
        }
    }
}