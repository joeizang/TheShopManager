using LanguageExt.Common;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Abstractions;

public interface ITenantCommandService
{
    Task<Result<TenantDto>> CreateTenant(CreateTenantDto inputModel);
    
    Task<TenantDto> UpdateTenant(UpdateTenantDto inputModel);
    
    Task<IResult> DeleteTenant(Guid tenantId);
}