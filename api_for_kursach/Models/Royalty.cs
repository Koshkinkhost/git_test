using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Royalty
{
    public int RoyaltyId { get; set; }

    public int? CompositionId { get; set; }

    public int? AuthorId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? RotationId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Composition? Composition { get; set; }

    public virtual Rotation? Rotation { get; set; }
}
