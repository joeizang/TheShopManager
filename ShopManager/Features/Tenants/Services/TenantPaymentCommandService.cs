using LanguageExt;
using LanguageExt.Common;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;
using Microsoft.EntityFrameworkCore;
using ShopManager.CustomExceptions;

namespace ShopManager.Features.Tenants.Services;

public class TenantPaymentCommandService(ShopManagerBaseContext context) : ITenantPaymentCommandService
{
    public async Task<Option<TenantPaymentDto>> CreateTenantPaymentAsync(CreateTenantPaymentDto dto)
    {
        var tpayment = dto.MapToTenantPayment();
        context.TenantPayments.Add(tpayment);
        await context.SaveChangesAsync();
        var newPayment = await context
            .TenantPayments.AsNoTracking()
            .OrderByDescending(tp => tp.CreatedAt)
            .Where(tp => tp.Id.Equals(tpayment.Id))
            .Select(tp => new TenantPaymentDto(
                tp.TenantId, tp.TenantInvoiceId, tp.PaymentMethodId, tp.PaymentReference,
                tp.Description, tp.AmountPaid.Amount, tp.Status))
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);
        return newPayment is not null ? Option<TenantPaymentDto>.Some(newPayment) : Option<TenantPaymentDto>.None;
    }

    public async Task<Result<TenantPaymentDto>> UpdateTenantPaymentAsync(UpdateTenantPaymentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Unit>> DeleteTenantPaymentAsync(Guid tenantPaymentId)
    {
        var tenantPayment = await context.TenantPayments.FindAsync(tenantPaymentId);
        if (tenantPayment is null)
        {
            return new Result<Unit>(new NotFoundException("Tenant payment not found"));
        }
        
        context.Entry(tenantPayment).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return new Result<Unit>(Unit.Default);
    }
}