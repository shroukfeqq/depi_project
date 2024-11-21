using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class LoginViewModel
    {
        //[Required]
        //public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
