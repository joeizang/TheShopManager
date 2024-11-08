using System;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace ShopManager.DomainModels;

public class ApplicationRole : IdentityRole
{
    public string RoleDescription { get; set; } = string.Empty;

    public string RoleCode { get; set; } = string.Empty;

    public bool ElevatedUser { get; set; }

    public ZonedDateTime CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant().InUtc();

    public ZonedDateTime UpdatedAt { get; set; }
}
