using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.ViewModels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public int? AlbumId { get; set; }
        public string Track_Artist { get; set; }
        [Required]
        [AllowedValues("Rock","Pop","Jazz","Classical","Hip-Hop","Electronic","Blues")]
        public string Genre_track { get; set; }
        public int Listeners_count { get; set; }
    }
}
