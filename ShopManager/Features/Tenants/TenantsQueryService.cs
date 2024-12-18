using LanguageExt.Common;
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
    
    public static readonly Func<ShopManagerBaseContext, Guid, Task<TenantDto?>>
        GetTenantById = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Guid tenantId) => 
                context.Tenants.AsNoTracking().Where(x => x.Id.Equals(tenantId))
                    .Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                        t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                        t.PhoneNumber, t.EmailAddress, t.ContactName))
                    .SingleOrDefault());
    
    public static readonly Func<ShopManagerBaseContext, Guid, IAsyncEnumerable<SubscriptionPlanDto>>
        GetSubscriptionPlanForTenant = EF.CompileAsyncQuery(
                (ShopManagerBaseContext context, Guid tenantId) => 
                context.SubscriptionPlans.AsNoTracking()
                    .Include(s => s.SubscriptionPlanType)
                    .Where(s => s.TenantId.Equals(tenantId))
                    .Select(s => new SubscriptionPlanDto(
                        s.TenantId, s.Tenant.Name, s.SubscriptionPlanTypeId, s.SubscriptionPlanType.Name,
                        s.SubscriptionPlanType.Price.Amount, s.SubscriptionPlanType.Price.Currency,
                        s.SubscriptionPlanType.BillingCycle, s.Status)));

    public static readonly Func<ShopManagerBaseContext, Guid, Task<Result<SubscriptionPlanDto?>>> 
        GetSubscriptionPlanById = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Guid subId) =>
            context.SubscriptionPlans.AsNoTracking()
                .Include(s => s.SubscriptionPlanType)
                .Include(s => s.Tenant)
                .Where(s => s.Id.Equals(subId))
                .Select(s => new SubscriptionPlanDto(
                    s.TenantId, s.Tenant.Name, s.SubscriptionPlanTypeId, s.SubscriptionPlanType.Name,
                    s.SubscriptionPlanType.Price.Amount, s.SubscriptionPlanType.Price.Currency,
                    s.SubscriptionPlanType.BillingCycle, s.Status)
                    ).SingleOrDefault().ProjectToSubscriptionPlanDtoResult());

    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<SubscriptionPlanDto>>
        GetSubscriptionPlans = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .OrderBy(sp => sp.Id)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status)));

    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<SubscriptionPlanDto>>
        GetCursoredSubscriptionPlans = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Instant cursor)=>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .Where(sp => sp.CreatedAt > cursor)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status))
                    .Take(10));
}