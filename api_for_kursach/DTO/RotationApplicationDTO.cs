namespace api_for_kursach.DTO
{
    public class RotationApplicationDTO
    {
        public object ApplicationId { get; set; }
        public string TrackTitle { get; set; }
        public string ArtistName { get; set; }
        public string RadioStationName { get; set; }
        public string Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Notes { get; set; }
    }
}
