namespace api_for_kursach.DTOs
{
    public class StudioDTO
    {
        public int StudioId { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public int? FoundedYear { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Building { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
