using System;
using System.Collections.Generic;

namespace LibraryProject.Core.Models;

public partial class Borrowing
{
    public decimal BorrowingId { get; set; }

    public decimal MemberId { get; set; }

    public decimal BookId { get; set; }

    public decimal RuleId { get; set; }

    public decimal? ApprovedBy { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? BorrowDate { get; set; }

    public DateTime ExpectedReturnDate { get; set; }

    public DateTime? ActualReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal FineAmount { get; set; }

    public string FineStatus { get; set; } = null!;

    public DateTime? PaidAt { get; set; }

    public string? Notes { get; set; }

    public virtual Login? ApprovedByNavigation { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual BorrowingRule Rule { get; set; } = null!;
}
