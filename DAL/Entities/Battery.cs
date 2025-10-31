using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Battery
{
    public int BatteryId { get; set; }

    public string? BatteryName { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public string? DetailInformation { get; set; }

    public virtual ICollection<Booking> BookingBatteries { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingNewUnits { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingOldUnits { get; set; } = new List<Booking>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
