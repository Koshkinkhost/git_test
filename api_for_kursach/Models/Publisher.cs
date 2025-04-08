using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContractTerms { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
