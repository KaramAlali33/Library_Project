using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Login
{
    public decimal LoginId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public decimal RoleId { get; set; }

    public decimal? MemberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Member? Member { get; set; }

    public virtual Role Role { get; set; } = null!;
}
