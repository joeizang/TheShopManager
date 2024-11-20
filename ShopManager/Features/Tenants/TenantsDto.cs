using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants;

public record CreateTenantDto(string Name, string ContactName, string EmailAddress, string PhoneNumber, string Address,
    string BillingAddress, bool ActivationStatus, ZonedDateTime? NextBillingDate,
    ZonedDateTime? SubscriptionStartDate, ZonedDateTime? SubscriptionEndDate,
    PaymentStatus PaymentStatus = PaymentStatus.UNINITIALIZED);

public record TenantDto();    