using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Track
{
    public int TrackId { get; set; }

    public int ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public int? Duration { get; set; }

    public int? GenreId { get; set; }
    public string AudioUrl {  get; set; }

    public int PlaysCount { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual ICollection<ListeningHistory> ListeningHistories { get; set; } = new List<ListeningHistory>();

    public virtual ICollection<RotationApplication> RotationApplications { get; set; } = new List<RotationApplication>();

    public virtual ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();
}
