using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class CompositionAuthor
{
    public int CompositionId { get; set; }

    public int AuthorId { get; set; }

    public string Role { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual Composition Composition { get; set; } = null!;
}
