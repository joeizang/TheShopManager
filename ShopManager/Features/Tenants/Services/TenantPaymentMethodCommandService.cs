using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Services;

public class TenantPaymentMethodCommandService(ShopManagerBaseContext context) : ITenantPaymentMethodCommandService
{
    public async Task<Result<TenantPaymentMethodDto>> CreateTenantPaymentMethodAsync(
        CreateTenantPaymentMethodDto dto)
    {
        var tenantPaymentMethod = dto.MapToCreateTenantPaymentMethodDto();
        context.TenantPaymentMethods.Add(tenantPaymentMethod);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<TenantPaymentMethodDto>(tenantPaymentMethod.MapToTenantPaymentMethodDto());
    }

    public async Task<Option<TenantPaymentMethodDto>> UpdateTenantPaymentMethodAsync(
        UpdateTenantPaymentMethodDto dto)
    {
        var tenantPaymentMethod = await context.TenantPaymentMethods
            .FindAsync(dto.PaymentMethodId)
            .ConfigureAwait(false);
        if (tenantPaymentMethod is null)
        {
            return Option<TenantPaymentMethodDto>.None;
        }

        tenantPaymentMethod.UpdatePaymentMethod(dto);
        context.Entry(tenantPaymentMethod).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return Option<TenantPaymentMethodDto>.Some(tenantPaymentMethod.MapToTenantPaymentMethodDto());
    }

    public async Task<Option<Unit>> DeleteTenantPaymentMethodAsync(Guid tenantPaymentMethodId)
    {
        var tenantPaymentMethod = await context.TenantPaymentMethods
            .FindAsync(tenantPaymentMethodId)
            .ConfigureAwait(false);
        if (tenantPaymentMethod is null)
        {
            return Option<Unit>.None;
        }

        tenantPaymentMethod.IsDeleted = true;
        context.Entry(tenantPaymentMethod).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return Option<Unit>.Some(Unit.Default);
    }
}