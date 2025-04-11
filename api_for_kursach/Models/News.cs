using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImagePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ArtistId { get; set; }

    public int? AdminId { get; set; }

    public virtual User? Admin { get; set; }

    public virtual Artist? Artist { get; set; }
}
