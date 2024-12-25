using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record UpdateTenantDto(Guid TenantId, string Name, string ContactName, string EmailAddress,
    string PhoneNumber, string Address, string BillingAddress, ActivationStatus ActivationStatus, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate, PaymentStatus PaymentStatus,
    Guid SubscriptionTypeId);