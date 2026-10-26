using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Publisher
{
    public decimal PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
