using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public class TenantCommandService(ShopManagerBaseContext contextI) : ITenantCommandService
{
    private readonly ShopManagerBaseContext _contextI = contextI;

    public Task<TenantDto> CreateTenant(CreateTenantDto inputModel)
    {
        
    }
}