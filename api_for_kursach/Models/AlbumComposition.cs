using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class AlbumComposition
{
    public int AlbumId { get; set; }

    public int CompositionId { get; set; }

    public int TrackNumber { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Composition Composition { get; set; } = null!;
}
