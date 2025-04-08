using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Rotation
{
    public int RotationId { get; set; }

    public int? CompositionId { get; set; }

    public int? RadioStationId { get; set; }

    public DateTime? AirDate { get; set; }

    public TimeOnly? AirTime { get; set; }

    public string? Region { get; set; }

    public virtual Composition? Composition { get; set; }

    public virtual RadioStation? RadioStation { get; set; }

    public virtual ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();
}
