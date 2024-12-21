using Microsoft.AspNetCore.Identity;
using NodaTime;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.DomainModels;

public class ApplicationUser : IdentityUser
{
    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public bool IsDeleted { get; set; }
    
    public uint Version { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

    public Instant UpdatedAt { get; set; }


}
