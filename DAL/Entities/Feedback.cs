using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int StationId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
