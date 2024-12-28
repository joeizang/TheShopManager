using LanguageExt;
using LanguageExt.Common;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Services;

public class TenantPaymentCommandService(ShopManagerBaseContext context) : ITenantPaymentCommandService
{
    public async Task<Result<TenantPaymentDto>> CreateTenantPaymentAsync(CreateTenantPaymentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TenantPaymentDto>> UpdateTenantPaymentAsync(UpdateTenantPaymentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Unit>> DeleteTenantPaymentAsync(Guid tenantPaymentId)
    {
        throw new NotImplementedException();
    }
}