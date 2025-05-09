namespace api_for_kursach.DTO
{
    public class TrackAlbumDTO
    {
        public string Title { get; set; } = null!;
        public int ArtistId {  get; set; }
        public int? Duration { get; set; }
        public int? GenreId { get; set; }
        public IFormFile? File { get; set; }
        public DateOnly ReleaseDate {  get; set; }
        public List<TrackSimpleDTO> Tracks { get; set; } = new();
    }
}
