using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Author
{
    public decimal AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
