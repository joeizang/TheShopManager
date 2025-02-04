using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Core;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.Sales.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
{
    private readonly IShopManagerContextAccessor _shopManagerContextAccessor;

    public SaleEntityTypeConfiguration(IShopManagerContextAccessor shopManagerContextAccessor)
    {
        _shopManagerContextAccessor = shopManagerContextAccessor;
    }

    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasQueryFilter(x => x.ShopId == _shopManagerContextAccessor.ShopId);
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.SaleDate)
            .IsRequired();
        
        builder.Property(x => x.Version).IsRowVersion();

        builder.ComplexProperty(s => s.TotalAmount)
            .IsRequired();

        builder.HasOne(s => s.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CustomerId);

        builder.HasMany(s => s.SaleItems)
            .WithOne()
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s => s.SalesPerson)
            .WithMany()
            .HasForeignKey(s => s.SalesPersonId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(s => s.Shop)
            .WithMany()
            .HasForeignKey(s => s.ShopId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(p => p.ShopId);

        builder.HasIndex(p => p.CustomerId);

        builder.HasIndex(p => p.SalesPersonId);

        builder.HasIndex(p => p.SaleDate);

        builder.HasIndex(p => p.CreatedAt);
    }
}
