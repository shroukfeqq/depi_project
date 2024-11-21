using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required] 
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MinLength(11),MaxLength(11)]
        public string Phone { get; set; }

    }
}
