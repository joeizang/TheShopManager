using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public class TenantCommandService(ShopManagerBaseContext contextI) : ITenantCommandService
{
    private readonly ShopManagerBaseContext _contextI = contextI;

    public async Task<TenantDto> CreateTenant(CreateTenantDto inputModel)
    {
        var tenant = inputModel.MapToTenant();
        _contextI.Tenants.Add(tenant);
        await _contextI.SaveChangesAsync().ConfigureAwait(false);
        return tenant.MapToTenantDto();
    }
}