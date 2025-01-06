using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string Discriminator { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public string? RoleCode { get; set; }

    public bool? ElevatedUser { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
