namespace api_for_kursach.DTO
{
    public class TrackSimpleDTO
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public int? AlbumId {  get; set; }
        public int ArtistId {  get; set; }
        public string Track_Artist {  get; set; }
        public string? URL {  get; set; }
        public string Genre_track {  get; set; }
        public int Listeners_count {  get; set; }
        public string  FileBase64 {  get; set; }
    }
}
