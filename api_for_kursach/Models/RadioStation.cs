using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class RadioStation
{
    public int RadioStationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Frequency { get; set; }

    public string? Country { get; set; }

    public string? ContactInfo { get; set; }

    public virtual ICollection<Rotation> Rotations { get; set; } = new List<Rotation>();
}
