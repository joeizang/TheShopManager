using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(s => s.ShopName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(s => s.ShopDescription)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(s => s.ShopAddress)
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.Property(s => s.ShopPhoneNumber)
            .HasMaxLength(22)
            .IsRequired();
        
        builder.Property(s => s.ShopEmailAddress)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(s => s.ShopLogo)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(s => s.Status)
            .IsRequired();
    }
}
