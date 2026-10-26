using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Category
{
    public decimal CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
