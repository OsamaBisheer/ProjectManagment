using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace ProjectManagment.Domain.Entities
{
    [Index(nameof(Title), IsUnique = true)]
    public class Task : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "tinyint")]
        public TaskStatusEnum Status { get; set; }

        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}