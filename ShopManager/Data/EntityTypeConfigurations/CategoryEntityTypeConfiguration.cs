using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CategoryName).IsRequired();
        builder.Property(x => x.CategoryDescription).IsRequired();
        builder.HasOne(x => x.Shop)
            .WithMany()
            .HasForeignKey(x => x.ShopId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => x.ShopId);
        builder.HasIndex(x => x.CategoryName);
    }
}
