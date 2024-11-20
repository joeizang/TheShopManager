namespace ShopManager.Features.Tenants;

public interface ITenantCommandService
{
    Task<TenantDto> CreateTenant(CreateTenantDto inputModel);
}