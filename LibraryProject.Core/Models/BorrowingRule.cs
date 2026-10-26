using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class BorrowingRule
{
    public decimal RuleId { get; set; }

    public string RuleName { get; set; } = null!;

    public decimal MaxBorrowDays { get; set; }

    public decimal MaxBooksPerMember { get; set; }

    public decimal FinePerDay { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
