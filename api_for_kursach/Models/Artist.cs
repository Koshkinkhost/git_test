using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? Country { get; set; }

    public string? ContactInfo { get; set; }

    public bool IsActive { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
