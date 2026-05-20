using ProjectManagment.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Domain.ViewModels.Task
{
    public class TaskCreateVM : AuditableVM
    {
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