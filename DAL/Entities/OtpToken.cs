using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class OtpToken
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool Used { get; set; }

    public int Attempts { get; set; }
}
