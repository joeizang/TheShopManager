using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record TenantDto(string Name, PaymentStatus PaymentStatus, Instant SubscriptionEndDate, 
    Instant SubscriptionStartDate, Instant NextBillingDate, ActivationStatus ActivationStatus, 
    string BillingAddress, string Address, string PhoneNumber, string EmailAddress, string ContactName);

public record TenantShopDto(Guid ShopId, string ShopName);    