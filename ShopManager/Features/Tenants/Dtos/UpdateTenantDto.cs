using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants;

public record UpdateTenantDto(Guid TenantId, string Name, string ContactName, string EmailAddress,
    string PhoneNumber, string Address, string BillingAddress, bool ActivationStatus, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate, PaymentStatus PaymentStatus);