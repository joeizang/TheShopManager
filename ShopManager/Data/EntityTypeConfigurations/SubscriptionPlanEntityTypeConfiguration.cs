using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SubscriptionPlanEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(sp => sp.Id);
        
        builder.Property(sp => sp.Id)
            .ValueGeneratedOnAdd();

        builder.Property(sp => sp.Status)
            .HasDefaultValue(ActivationStatus.INACTIVE)
            .IsRequired();

        builder.Property(sp => sp.CreatedAt)
            .IsRequired();
        
        builder.HasOne(sp => sp.Tenant)
            .WithMany(t => t.SubscriptionPlans)
            .HasForeignKey(sp => sp.TenantId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(sp => sp.SubscriptionPlanType)
            .WithMany(spt => spt.SubscriptionPlans)
            .HasForeignKey(sp => sp.SubscriptionPlanTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}