using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants;

public record CreateTenantDto(string Name, string ContactName, string EmailAddress, string PhoneNumber, string Address,
    string BillingAddress, bool ActivationStatus, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate,
    PaymentStatus PaymentStatus = PaymentStatus.UNINITIALIZED);

public record TenantDto(string Name, PaymentStatus PaymentStatus, Instant SubscriptionEndDate, 
    Instant SubscriptionStartDate, Instant NextBillingDate, bool ActivationStatus, 
    string BillingAddress, string Address, string PhoneNumber, string EmailAddress, string ContactName);
    
public record DeleteTenantDto(Guid TenantId);    