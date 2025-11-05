using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public int StationId { get; set; }

    public int? PackageId { get; set; }

    public DateTime TimeDate { get; set; }

    public string? Record { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionType { get; set; }

    public int? BookingId { get; set; }

    public string Status { get; set; } = null!;

    public string? TransactionRef { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual PackagePlan? Package { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
