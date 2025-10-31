using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class RevokedToken
{
    public long Id { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string? Username { get; set; }
}
