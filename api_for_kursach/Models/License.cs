using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class License
{
    public int LicenseId { get; set; }

    public int TrackId { get; set; }

    public int PublisherId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Territory { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
