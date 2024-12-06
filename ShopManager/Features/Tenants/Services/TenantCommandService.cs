using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;

namespace ShopManager.Features.Tenants;

public class TenantCommandService(ShopManagerBaseContext context) : ITenantCommandService
{
    private readonly ShopManagerBaseContext _context = context;

    public async Task<TenantDto> CreateTenant(CreateTenantDto inputModel)
    {
        var tenant = inputModel.MapToTenant();
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return tenant.MapToTenantDto();
    }
    
    public async Task<TenantDto> UpdateTenant(UpdateTenantDto inputModel)
    {
        var tenant = inputModel.MapToTenant();
        _context.Entry(tenant).State = EntityState.Modified;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return tenant.MapToTenantDto();
    }

    public async Task<IResult> DeleteTenant(Guid tenantId)
    {
        var target = await _context.Tenants.FindAsync(tenantId).ConfigureAwait(false);
        if (target is null)
        {
            return TypedResults.NotFound();
        }

        target.IsDeleted = true;
        _context.Entry(target).State = EntityState.Modified;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return TypedResults.NoContent();
    }
}