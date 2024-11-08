using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace ShopManager.DomainModels;

public class ApplicationUser : IdentityUser
{
    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public ZonedDateTime CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant().InUtc();

    public ZonedDateTime UpdatedAt { get; set; }


}
