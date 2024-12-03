using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public static class TenantsQueryService
{
    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<TenantDto>>
        GetTenants = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.Tenants.AsNoTracking().Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                    t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                    t.PhoneNumber, t.EmailAddress, t.ContactName)));

    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<TenantDto>>
        GetCursoredTenants = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Instant cursor) => 
                context.Tenants.AsNoTracking()
                    .Where(t => t.CreatedAt > cursor)
                    .Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                        t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                        t.PhoneNumber, t.EmailAddress, t.ContactName))
                    .Take(10));
}