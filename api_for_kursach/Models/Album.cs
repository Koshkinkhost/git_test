using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public int? ReleaseYear { get; set; }

    public int? PublisherId { get; set; }

    public virtual ICollection<AlbumComposition> AlbumCompositions { get; set; } = new List<AlbumComposition>();

    public virtual Publisher? Publisher { get; set; }
}
