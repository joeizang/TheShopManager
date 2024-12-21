using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Services;

public class TenantCommandService(ShopManagerBaseContext context) : ITenantCommandService
{
    public async Task<Result<TenantDto>> CreateTenant(CreateTenantDto inputModel)
    {
        var tenant = inputModel.MapToTenant();
        context.Tenants.Add(tenant);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<TenantDto>(tenant.MapToTenantDto());
    }
    
    public async Task<TenantDto> UpdateTenant(UpdateTenantDto inputModel)
    {
        var tenant = inputModel.MapToTenant();
        context.Entry(tenant).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return tenant.MapToTenantDto();
    }

    public async Task<IResult> DeleteTenant(Guid tenantId)
    {
        var target = await context.Tenants.FindAsync(tenantId).ConfigureAwait(false);
        if (target is null)
        {
            return TypedResults.NotFound();
        }

        target.IsDeleted = true;
        context.Entry(target).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return TypedResults.NoContent();
    }
}