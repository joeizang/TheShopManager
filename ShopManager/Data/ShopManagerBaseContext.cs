using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopManager.DomainModels;

namespace ShopManager.Data;

public class ShopManagerBaseContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
}
