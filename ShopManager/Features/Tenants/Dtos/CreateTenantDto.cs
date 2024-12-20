using ShopManager.DomainModels;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants;

public record CreateTenantDto(string Name, string ContactName, string EmailAddress, string PhoneNumber, string Address,
    string BillingAddress, bool ActivationStatus, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate,
    PaymentStatus PaymentStatus = PaymentStatus.UNINITIALIZED);