using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Book
{
    public decimal BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public string? Description { get; set; }

    public string? CoverImage { get; set; }

    public byte? PublicationYear { get; set; }

    public decimal TotalCopies { get; set; }

    public decimal AvailableCopies { get; set; }

    public bool? IsFeatured { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal CategoryId { get; set; }

    public decimal PublisherId { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Category Category { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
