using api_for_kursach.Models;

namespace api_for_kursach.DTO
{
    public class RadioDTO
    {
        public int RadioStationId { get; set; }

        public string Name { get; set; } 

        public string Frequency { get; set; }

        public string? Country { get; set; }

        public string? ContactInfo { get; set; }

        public List<RotationApplication> rotations;
    }
}
