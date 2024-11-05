using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopManager.DomainModels;

namespace ShopManager.Data;

public class ShopManagerBaseContext : IdentityDbContext<ApplicationUser>
{
    public ShopManagerBaseContext(DbContextOptions options) : base(options)
    {
    }
}
