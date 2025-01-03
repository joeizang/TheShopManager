using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record CreateTenantDto(string Name, string ContactName, string EmailAddress, string PhoneNumber, string Address,
    string BillingAddress, string NextBillingDate,
    string SubscriptionStartDate, string SubscriptionEndDate, Guid SubscriptionPlanTypeId);