using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BatteryModel
{
    public int ModelId { get; set; }

    public string? ModelName { get; set; }

    public int? Capacity { get; set; }

    public int? Voltage { get; set; }
}
