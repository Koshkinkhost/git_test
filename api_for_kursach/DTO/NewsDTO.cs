namespace api_for_kursach.DTO
{
    public class NewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } 
        public string? ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ArtistId { get; set; }
        public int? AdminId { get; set; }
    }
}
