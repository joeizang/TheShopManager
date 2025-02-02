using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Core;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class FairlyUsedItemEntityTypeConfiguration : IEntityTypeConfiguration<FairlyUsedItem>
{
    private readonly IShopManagerContextAccessor _shopManagerContextAccessor;

    public FairlyUsedItemEntityTypeConfiguration(IShopManagerContextAccessor shopManagerContextAccessor)
    {
        _shopManagerContextAccessor = shopManagerContextAccessor;
    }

    public void Configure(EntityTypeBuilder<FairlyUsedItem> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasQueryFilter(x => x.ShopId == _shopManagerContextAccessor.ShopId);
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(f => f.DateBought)
            .IsRequired();
        
        builder.Property(x => x.Version).IsRowVersion();

        builder.Property(f => f.ItemName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(f => f.ItemDescription)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(f => f.ItemDetails)
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.ComplexProperty(f => f.Price)
            .IsRequired();

        builder.HasOne(f => f.Customer)
            .WithMany()
            .HasForeignKey(f => f.CustomerId)
            .IsRequired();
        
        builder.HasOne(f => f.Product)
            .WithMany()
            .HasForeignKey(f => f.ProductId)
            .IsRequired();
        
        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .IsRequired();

        builder.HasOne(f => f.Shop)
            .WithMany()
            .HasForeignKey(f => f.ShopId)
            .IsRequired();

        builder.HasIndex(f => f.CreatedAt);
        builder.HasIndex(f => f.ShopId);
        builder.HasIndex(f => f.CustomerId);
        
        
    }
}
