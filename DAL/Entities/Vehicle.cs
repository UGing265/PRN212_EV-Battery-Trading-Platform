using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int UserId { get; set; }

    public string Vin { get; set; } = null!;

    public string? VehicleModel { get; set; }

    public string? BatteryType { get; set; }

    public string? RegisterInformation { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User User { get; set; } = null!;
}
