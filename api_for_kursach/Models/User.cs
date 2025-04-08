using System;
using System.Collections.Generic;

namespace api_for_kursach.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? ArtistId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public int? RoleId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
