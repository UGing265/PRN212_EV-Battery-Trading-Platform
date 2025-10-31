using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int StationId { get; set; }

    public int UserId { get; set; }

    public int VehicleId { get; set; }

    public DateTime TimeDate { get; set; }

    public int? BatteryId { get; set; }

    public decimal? EstimatedPrice { get; set; }

    public decimal? DepositAmount { get; set; }

    public string? DepositStatus { get; set; }

    public string Status { get; set; } = null!;

    public string? CancelReason { get; set; }

    public DateTime? CanceledAt { get; set; }

    public int? DepositTxnId { get; set; }

    public DateTime? HoldUntil { get; set; }

    public int? OldUnitId { get; set; }

    public int? NewUnitId { get; set; }

    public virtual Battery? Battery { get; set; }

    public virtual Transaction? DepositTxn { get; set; }

    public virtual Battery? NewUnit { get; set; }

    public virtual Battery? OldUnit { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
