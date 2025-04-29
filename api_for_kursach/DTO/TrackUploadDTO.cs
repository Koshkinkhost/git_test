namespace api_for_kursach.DTO
{
    public class TrackUploadDTO
    {
        public int? TrackId { get; set; } // Nullable integer
        public string Title { get; set; }
        public int? AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string genreTrack { get; set; }
        public int Listeners_count { get; set; }
    }
}
