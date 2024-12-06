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

public record UpdateTenantDto(Guid TenantId, string Name, string ContactName, string EmailAddress,
    string PhoneNumber, string Address, string BillingAddress, bool ActivationStatus, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate, PaymentStatus PaymentStatus);
    
public record DeleteTenantDto(Guid TenantId);

public record SubscriptionPlanTypeDto(string Name, string Description, decimal Price, 
    string Features, decimal Discount);

public record CreateSubscriptionPlanTypeDto(string Name, string Description, decimal Price,
    string Features, decimal Discount);

public record UpdateSubscriptionPlanDto(Guid SubscriptionPlanTypeId, Guid TenantId, Guid SubscriptionPlanId);

public record SubscriptionPlanDto(Guid TenantId, Guid SubscriptionPlanTypeId, string SubscriptionPlanTypeName,
    Money Price, BillingCycle BillingCycle, ActivationStatus Status);

public record CreateSubscriptionPlanDto(Guid TenantId, Guid SubscriptionPlanTypeId);    