using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.Models
{
    public class Studio
    {
        [Key]
        public int Id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string phone_num {  get; set; }
        public string email {  get; set; }
        public string street {  get; set; }
        public string city { get; set; }
        public string build {  get; set; }
    }
}
