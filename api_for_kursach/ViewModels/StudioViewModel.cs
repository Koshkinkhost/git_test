using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.ViewModels
{
    public class StudioViewModel
    {
        [Required(ErrorMessage = "Latitude is required.")]
        public string lat { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        public string longt { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string phone_num { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string city { get; set; }





        [Required(ErrorMessage = "Building number is required.")]
        public string build { get; set; }
    }
}
