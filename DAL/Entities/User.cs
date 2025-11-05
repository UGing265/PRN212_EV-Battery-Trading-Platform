using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public string? Address { get; set; }

    public int? StationId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Station? Station { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public int Id { get; set; }
    public decimal? TotalCredit { get; set; }
    public int RoleId { get;set; }
}
