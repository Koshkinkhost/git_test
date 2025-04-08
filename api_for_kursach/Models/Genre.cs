using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Composition> Compositions { get; set; } = new List<Composition>();
}
