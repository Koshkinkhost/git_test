namespace api_for_kursach.DTO
{
    public class TrackUpdatedDTO
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public string GenreName { get; set; } // <--- не Id, а название
        public string TrackArtist { get; set; }
        public int AlbumId { get; set; }
    }
}
