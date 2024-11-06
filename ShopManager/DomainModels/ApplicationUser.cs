using Microsoft.AspNetCore.Identity;

namespace ShopManager.DomainModels;

public class ApplicationUser : IdentityUser
{
    public Guid ShopId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;


}
