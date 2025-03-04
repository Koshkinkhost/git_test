using api_for_kursach.Models;
using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password field is required")]
       
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

    }
}
