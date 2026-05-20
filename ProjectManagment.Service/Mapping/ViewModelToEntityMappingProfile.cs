using AutoMapper;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.ViewModels.Project;
using ProjectManagment.Domain.ViewModels.Task;

namespace ProjectManagment.Service.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ProjectCreateVM, Project>().AfterMap((vm, entity) => entity.SetCreated(entity.CreatedByUserId, DateTime.UtcNow));
            CreateMap<ProjectUpdateVM, Project>().AfterMap((vm, entity) => entity.SetLastUpdated(entity.LastUpdatedByUserId, DateTime.UtcNow));

            CreateMap<TaskCreateVM, Domain.Entities.Task>().AfterMap((vm, entity) => entity.SetCreated(entity.CreatedByUserId, DateTime.UtcNow));
            CreateMap<TaskUpdateVM, Domain.Entities.Task>().AfterMap((vm, entity) => entity.SetLastUpdated(entity.LastUpdatedByUserId, DateTime.UtcNow));
        }
    }
}