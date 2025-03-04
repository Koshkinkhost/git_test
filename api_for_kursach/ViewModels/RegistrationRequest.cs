using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.ViewModels
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Login si required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 symbols")]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Passwords must be identical")]
        public string ConfirmPassword { get; set; }
       
    }
}
