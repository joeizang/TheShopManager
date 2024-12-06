namespace ShopManager.Features.Tenants.Abstractions;

public interface ITenantCommandService
{
    Task<TenantDto> CreateTenant(CreateTenantDto inputModel);
    
    Task<TenantDto> UpdateTenant(UpdateTenantDto inputModel);
    
    Task<IResult> DeleteTenant(Guid tenantId);
}