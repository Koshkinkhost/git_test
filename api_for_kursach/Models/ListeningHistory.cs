using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class ListeningHistory
{
    public int ListeningId { get; set; }

    public int UserId { get; set; }

    public int TrackId { get; set; }

    public DateTime PlayDate { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
