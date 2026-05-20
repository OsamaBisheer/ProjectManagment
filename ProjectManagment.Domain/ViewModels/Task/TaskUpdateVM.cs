using ProjectManagment.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Domain.ViewModels.Task
{
    public class TaskUpdateVM : AuditableVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}