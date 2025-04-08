using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Composition
{
    public int CompositionId { get; set; }

    public string Title { get; set; } = null!;

    public TimeOnly? Duration { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? GenreId { get; set; }

    public string? Lyrics { get; set; }

    public virtual ICollection<AlbumComposition> AlbumCompositions { get; set; } = new List<AlbumComposition>();

    public virtual ICollection<CompositionAuthor> CompositionAuthors { get; set; } = new List<CompositionAuthor>();

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual ICollection<RotationApplication> RotationApplications { get; set; } = new List<RotationApplication>();

    public virtual ICollection<Rotation> Rotations { get; set; } = new List<Rotation>();

    public virtual ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();
}
