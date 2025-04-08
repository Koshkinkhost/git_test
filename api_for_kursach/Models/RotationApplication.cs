using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class RotationApplication
{
    public int ApplicationId { get; set; }

    public int? CompositionId { get; set; }

    public int? AuthorId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? SubmissionDate { get; set; }

    public DateTime? ReviewDate { get; set; }

    public string? Notes { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Composition? Composition { get; set; }
}
