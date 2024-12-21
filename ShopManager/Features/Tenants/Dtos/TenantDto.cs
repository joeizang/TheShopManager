using NodaTime;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record TenantDto(string Name, PaymentStatus PaymentStatus, Instant SubscriptionEndDate, 
    Instant SubscriptionStartDate, Instant NextBillingDate, bool ActivationStatus, 
    string BillingAddress, string Address, string PhoneNumber, string EmailAddress, string ContactName);