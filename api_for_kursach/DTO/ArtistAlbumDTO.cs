namespace api_for_kursach.DTO
{
    public class ArtistAlbumDTO
    {
        public int ArtistId { get; set; }   
        public string Artist {  get; set; }
        public string Title { get; set; }
        public DateOnly RealeseDate { get; set; }
    }
}
