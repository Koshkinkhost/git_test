using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? Country { get; set; }

    public string? ContactInfo { get; set; }

    public virtual ICollection<CompositionAuthor> CompositionAuthors { get; set; } = new List<CompositionAuthor>();

    public virtual ICollection<RotationApplication> RotationApplications { get; set; } = new List<RotationApplication>();

    public virtual ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();
}
