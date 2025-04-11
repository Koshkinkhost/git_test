using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class RotationApplication
{
    public int ApplicationId { get; set; }

    public int TrackId { get; set; }

    public int RadioStationId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }

    public DateTime? ReviewDate { get; set; }

    public string? Notes { get; set; }

    public virtual RadioStation RadioStation { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
