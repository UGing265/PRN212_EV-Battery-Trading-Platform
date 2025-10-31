using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Station
{
    public int StationId { get; set; }

    public string StationName { get; set; } = null!;

    public string? Address { get; set; }

    public string? StationStatus { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
