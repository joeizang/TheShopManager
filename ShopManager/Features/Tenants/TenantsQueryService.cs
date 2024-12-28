using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Data;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants;

public static class TenantsQueryService
{
    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<TenantDto>>
        GetTenantsAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.Tenants.AsNoTracking().Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                    t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                    t.PhoneNumber, t.EmailAddress, t.ContactName)));
    
    public static readonly Func<ShopManagerBaseContext, IEnumerable<TenantDto>>
        GetTenants = EF.CompileQuery(
            (ShopManagerBaseContext context) =>
                context.Tenants.AsNoTracking().Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                    t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                    t.PhoneNumber, t.EmailAddress, t.ContactName)));

    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<TenantDto>>
        GetCursoredTenantsAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Instant cursor) => 
                context.Tenants.AsNoTracking()
                    .Where(t => t.CreatedAt > cursor)
                    .Select(t => new TenantDto(t.Name, t.PaymentStatus, t.SubscriptionEndDate,
                        t.SubscriptionStartDate, t.NextBillingDate, t.ActivationStatus, t.BillingAddress, t.Address,
                        t.PhoneNumber, t.EmailAddress, t.ContactName))
                    .Take(10));
    
    public static readonly Func<ShopManagerBaseContext, Instant, IEnumerable<TenantDto>>
        GetCursoredTenants = EF.CompileQuery(
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
        GetSubscriptionPlanForTenantAsync = EF.CompileAsyncQuery(
                (ShopManagerBaseContext context, Guid tenantId) => 
                context.SubscriptionPlans.AsNoTracking()
                    .Include(s => s.SubscriptionPlanType)
                    .Where(s => s.TenantId.Equals(tenantId))
                    .Select(s => new SubscriptionPlanDto(
                        s.TenantId, s.Tenant.Name, s.SubscriptionPlanTypeId, s.SubscriptionPlanType.Name,
                        s.SubscriptionPlanType.Price.Amount, s.SubscriptionPlanType.Price.Currency,
                        s.SubscriptionPlanType.BillingCycle, s.Status)));
    
    public static readonly Func<ShopManagerBaseContext, Guid, IEnumerable<SubscriptionPlanDto>>
        GetSubscriptionPlanForTenant = EF.CompileQuery(
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
        GetSubscriptionPlansAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .OrderBy(sp => sp.Id)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status)));
    
    public static readonly Func<ShopManagerBaseContext, IEnumerable<SubscriptionPlanDto>>
        GetSubscriptionPlans = EF.CompileQuery(
            (ShopManagerBaseContext context) =>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .OrderBy(sp => sp.Id)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status)));

    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<SubscriptionPlanDto>>
        GetCursoredSubscriptionPlansAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Instant cursor)=>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .Where(sp => sp.CreatedAt > cursor)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status))
                    .Take(10));
    
    public static readonly Func<ShopManagerBaseContext, Instant, IEnumerable<SubscriptionPlanDto>>
        GetCursoredSubscriptionPlans = EF.CompileQuery(
            (ShopManagerBaseContext context, Instant cursor)=>
                context.SubscriptionPlans.AsNoTracking()
                    .Include(sp => sp.SubscriptionPlanType)
                    .Include(sp => sp.Tenant)
                    .Where(sp => sp.CreatedAt > cursor)
                    .Select(sp => new SubscriptionPlanDto(sp.TenantId, sp.Tenant.Name, sp.SubscriptionPlanTypeId,
                        sp.SubscriptionPlanType.Name, sp.SubscriptionPlanType.Price.Amount,
                        sp.SubscriptionPlanType.Price.Currency, sp.SubscriptionPlanType.BillingCycle, sp.Status))
                    .Take(10));

    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<SubscriptionPlanTypeDto>>
        GetSubscriptionPlanTypesAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context) =>
                context.SubscriptionPlanTypes.AsNoTracking()
                    .Select(spt => new SubscriptionPlanTypeDto(spt.Name, spt.Description, spt.Price.Amount,
                        spt.Price.Currency, spt.Features, spt.Discount))
                    .Take(10)
        );
    
    public static readonly Func<ShopManagerBaseContext, IEnumerable<SubscriptionPlanTypeDto>>
        GetSubscriptionPlanTypes = EF.CompileQuery(
            (ShopManagerBaseContext context) =>
                context.SubscriptionPlanTypes.AsNoTracking()
                    .Select(spt => new SubscriptionPlanTypeDto(spt.Name, spt.Description, spt.Price.Amount,
                        spt.Price.Currency, spt.Features, spt.Discount))
                    .Take(10)
        );

    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<SubscriptionPlanTypeDto>>
        GetCursoredSubscriptionPlanTypesAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Instant cursor) =>
                context.SubscriptionPlanTypes.AsNoTracking()
                    .Where(spt => spt.CreatedAt > cursor)
                    .Select(spt => new SubscriptionPlanTypeDto(spt.Name, spt.Description, spt.Price.Amount,
                        spt.Price.Currency, spt.Features, spt.Discount))
                    .Take(10));
    
    public static readonly Func<ShopManagerBaseContext, Instant, IEnumerable<SubscriptionPlanTypeDto>>
        GetCursoredSubscriptionPlanTypes = EF.CompileQuery(
            (ShopManagerBaseContext context, Instant cursor) =>
                context.SubscriptionPlanTypes.AsNoTracking()
                    .Where(spt => spt.CreatedAt > cursor)
                    .Select(spt => new SubscriptionPlanTypeDto(spt.Name, spt.Description, spt.Price.Amount,
                        spt.Price.Currency, spt.Features, spt.Discount))
                    .Take(10));

    public static readonly Func<ShopManagerBaseContext, Guid, Task<SubscriptionPlanTypeDto?>>
        GetSubscriptionPlanTypesById = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Guid subTypeId) =>
                context.SubscriptionPlanTypes.AsNoTracking()
                    .Include(spt => spt.SubscriptionPlans)
                    .Where(spt => spt.Id.Equals(subTypeId))
                    .Select(spt => new SubscriptionPlanTypeDto(spt.Name, spt.Description, spt.Price.Amount,
                        spt.Price.Currency, spt.Features, spt.Discount))
                    .SingleOrDefault()
        );

    public static readonly Func<ShopManagerBaseContext, Guid, IEnumerable<TenantPaymentDto>>
        GetTenantsPayments = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid tenantId) =>
                context.TenantPayments.AsNoTracking()
                    .Where(tp => tp.TenantId.Equals(tenantId))
                    .Select(tp => new TenantPaymentDto(tp.TenantId, tp.TenantInvoiceId,
                        tp.PaymentMethodId, tp.PaymentReference, tp.Description, 
                        tp.AmountPaid.Amount, tp.Status))
        );
    
    public static readonly Func<ShopManagerBaseContext, Guid, IAsyncEnumerable<TenantPaymentDto>>
        GetTenantsPaymentsAsync = EF.CompileAsyncQuery(
            (ShopManagerBaseContext context, Guid tenantId) =>
                context.TenantPayments.AsNoTracking()
                    .Where(tp => tp.TenantId.Equals(tenantId))
                    .Select(tp => new TenantPaymentDto(tp.TenantId, tp.TenantInvoiceId,
                        tp.PaymentMethodId, tp.PaymentReference, tp.Description, 
                        tp.AmountPaid.Amount, tp.Status))
        );
    
    public static readonly Func<ShopManagerBaseContext, Guid, TenantPaymentInfoDto?>
        GetTenantPaymentInfo = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid paymentId) =>
                context.TenantPayments.AsNoTracking()
                    .Include(tp => tp.Tenant)
                    .Include(tp => tp.TenantInvoice)
                    .Where(tp => tp.Id.Equals(paymentId))
                    .Select(tp => new TenantPaymentInfoDto(tp.TenantId, tp.Tenant.Name, tp.PaymentDate.ToString(),
                        tp.TenantInvoiceId, tp.TenantInvoice.CreatedAt.ToString(), tp.PaymentMethodId,
                        tp.PaymentReference, tp.Description, tp.AmountPaid.Amount, tp.Status))
                    .SingleOrDefault()
        );

    public static readonly Func<ShopManagerBaseContext, Guid, IEnumerable<TenantPaymentMethodDto>>
        GetTenantPaymentMethods = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid tenantId) =>
                context.TenantPaymentMethods.AsNoTracking()
                    .Include(tpm => tpm.Tenant)
                    .OrderByDescending(tpm => tpm.CreatedAt)
                    .Where(tpm => tpm.TenantId.Equals(tenantId))
                    .Select(tpm => new TenantPaymentMethodDto(tpm.TenantId, tpm.PaymentDetails,
                        tpm.PaymentMethod, tpm.IsDefaultPaymentMethod))
        );
}