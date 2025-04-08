using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class License
{
    public int LicenseId { get; set; }

    public int? CompositionId { get; set; }

    public int? PublisherId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Territory { get; set; }

    public virtual Composition? Composition { get; set; }

    public virtual Publisher? Publisher { get; set; }
}
