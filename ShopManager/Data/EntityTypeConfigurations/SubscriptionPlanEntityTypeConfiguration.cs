using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SubscriptionPlanEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(sp => sp.Id);
        builder.Property(sp => sp.Id)
            .ValueGeneratedOnAdd();

        builder.Property(sp => sp.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sp => sp.Description)
            .HasMaxLength(200);

        builder.ComplexProperty(sp => sp.Price)
            .IsRequired();

        builder.Property(sp => sp.BillingCycle)
            .IsRequired();

        builder.Property(sp => sp.Status)
            .HasDefaultValue(ActivationStatus.INACTIVE)
            .IsRequired();

        builder.Property(sp => sp.Features)
            .HasMaxLength(500);

        builder.Property(sp => sp.CreatedAt)
            .IsRequired();
        
        builder.HasOne(sp => sp.Tenant)
            .WithMany(t => t.SubscriptionPlans)
            .HasForeignKey(sp => sp.TenantId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}