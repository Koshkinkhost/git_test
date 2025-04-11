using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<ListeningHistory> ListeningHistories { get; set; } = new List<ListeningHistory>();

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
