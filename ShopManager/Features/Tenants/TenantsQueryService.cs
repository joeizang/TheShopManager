using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public static class TenantsQueryService
{
    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<TenantDto>>
        GetTenants = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.Tenants.Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                    t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                    t.PhoneNumber, t.EmailAddress, t.ContactName)));
}