using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password field is required")]
       
        public string Password { get; set; }
        
    }
}
