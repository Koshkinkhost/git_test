using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();

    public virtual Studio? Studio { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual User? User { get; set; }
}
