using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class Royalty
{
    public int RoyaltyId { get; set; }

    public int TrackId { get; set; }

    public int AuthorId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Artist Author { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
