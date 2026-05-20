using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.Domain.ViewModels.User
{
    public class UserAddVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}