using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SubscriptionPlanTypeEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPlanType>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanType> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();
        builder.ComplexProperty(x => x.Price)
            .IsRequired();
        builder.Property(x => x.BillingCycle)
            .IsRequired();
        builder.Property(x => x.Discount);
        
        builder.Property(x => x.Features)
            .HasMaxLength(1000)
            .IsRequired();
        builder.HasMany(x => x.SubscriptionPlans)
            .WithOne(x => x.SubscriptionPlanType)
            .HasForeignKey(x => x.SubscriptionPlanTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}