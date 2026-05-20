using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities.Common;

namespace ProjectManagment.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Project : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
    }
}