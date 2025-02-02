using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Core;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    private readonly IShopManagerContextAccessor _shopManagerContextAccessor;

    public CategoryEntityTypeConfiguration(IShopManagerContextAccessor shopManagerContextAccessor)
    {
        _shopManagerContextAccessor = shopManagerContextAccessor;
    }

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasQueryFilter(x => x.ShopId == _shopManagerContextAccessor.ShopId);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CategoryName).IsRequired();
        builder.Property(x => x.CategoryDescription).IsRequired();
        builder.HasOne(x => x.Shop)
            .WithMany()
            .HasForeignKey(x => x.ShopId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Version).IsRowVersion();
        
        builder.HasIndex(x => x.ShopId);
        builder.HasIndex(x => x.CategoryName);
    }
}
