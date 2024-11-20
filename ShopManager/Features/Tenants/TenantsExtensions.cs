using NodaTime;

namespace ShopManager.Features.Tenants;
using ShopManager.Features.Tenants.DomainModels;

public static class TenantsExtensions
{
    public static Tenant MapToTenant(this CreateTenantDto dto)
    {
        return new Tenant
        {
            Name = dto.Name,
            ContactName = dto.ContactName,
            EmailAddress = dto.EmailAddress,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            BillingAddress = dto.BillingAddress,
            ActivationStatus = dto.ActivationStatus,
            NextBillingDate = dto.NextBillingDate ?? ZonedDateTime.FromDateTimeOffset(DateTimeOffset.),
            SubscriptionStartDate = dto.SubscriptionStartDate ?? ZonedDateTime.FromDateTimeOffset(Date),
            SubscriptionEndDate = dto.SubscriptionEndDate ?? ZonedDateTime.FromDateTimeOffset(Date),
            PaymentStatus = dto.PaymentStatus
        };
    }
}