using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Studio
{
    public int StudioId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public int? FoundedYear { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? Building { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
