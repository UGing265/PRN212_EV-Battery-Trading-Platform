using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class PackagePlan
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? DurationDays { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
