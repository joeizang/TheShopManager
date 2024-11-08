﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopManager.DomainModels;

namespace ShopManager.Data;

public class ShopManagerBaseContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopManagerBaseContext).Assembly);
    }

    public DbSet<Shop> Shops { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<FairlyUsedItem> FairlyUsedItems { get; set; } = default!;
    public DbSet<Supplier> Suppliers { get; set; } = default!;
    public DbSet<Inventory> Inventories { get; set; } = default!;
    public DbSet<Sale> Sales { get; set; } = default!;
    public DbSet<SaleItem> SaleItems { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;
    
}
