using System.ComponentModel.DataAnnotations;
using ProjectManagment.Domain.ViewModels.Common;

namespace ProjectManagment.Domain.ViewModels.Project
{
    public class ProjectCreateVM : AuditableVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}