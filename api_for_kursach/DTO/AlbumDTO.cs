using api_for_kursach.ViewModels;

namespace api_for_kursach.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<TrackViewModel> Tracks { get; set; }


    }
}
