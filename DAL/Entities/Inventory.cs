using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int StationId { get; set; }

    public int BatteryId { get; set; }

    public string? Status { get; set; }

    public int ReadyQty { get; set; }

    public int HoldQty { get; set; }

    public int ChargingQty { get; set; }

    public int MaintenanceQty { get; set; }

    public virtual Battery Battery { get; set; } = null!;

    public virtual Station Station { get; set; } = null!;
}
