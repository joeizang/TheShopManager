using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Core;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    private readonly IShopManagerContextAccessor _shopManagerContextAccessor;

    public CustomerEntityTypeConfiguration(IShopManagerContextAccessor shopManagerContextAccessor)
    {
        _shopManagerContextAccessor = shopManagerContextAccessor;
    }

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasQueryFilter(x => x.ShopId == _shopManagerContextAccessor.ShopId);
        builder.HasKey(x => x.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Version).IsRowVersion();

        builder.Property(x => x.FirstName).IsRequired();

        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.EmailAddress)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(300);

        builder.HasOne(x => x.Shop)
            .WithMany()
            .HasForeignKey(x => x.ShopId);
        
        builder.HasIndex(x => x.PhoneNumber);

        builder.HasIndex(x => x.EmailAddress)
            .IsUnique();
        builder.HasIndex(x => x.ShopId);
        
        builder.HasIndex(x => new { x.FirstName , x.LastName });
    }
}
