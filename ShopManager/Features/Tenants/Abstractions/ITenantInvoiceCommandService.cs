using LanguageExt;
using LanguageExt.Common;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Abstractions;

public interface ITenantInvoiceCommandService
{
    Task<Result<TenantInvoiceDto>> CreateTenantInvoiceAsync(CreateTenantInvoiceDto dto);
    
    Task<Result<TenantInvoiceDto>> UpdateTenantInvoiceAsync(UpdateTenantInvoiceDto dto);
    
    Task<Result<Unit>> DeleteTenantInvoiceAsync(Guid tenantInvoiceId);
}

public interface ITenantPaymentCommandService
{
    Task<Option<TenantPaymentDto>> CreateTenantPaymentAsync(CreateTenantPaymentDto dto);
    
    Task<Result<TenantPaymentDto>> UpdateTenantPaymentAsync(UpdateTenantPaymentDto dto);
    
    Task<Result<Unit>> DeleteTenantPaymentAsync(Guid tenantPaymentId);
}

public interface ITenantPaymentMethodCommandService
{
    Task<Result<TenantPaymentMethodDto>> CreateTenantPaymentMethodAsync(CreateTenantPaymentMethodDto dto);
    
    Task<Option<TenantPaymentMethodDto>> UpdateTenantPaymentMethodAsync(UpdateTenantPaymentMethodDto dto);
    
    Task<Option<Unit>> DeleteTenantPaymentMethodAsync(Guid tenantPaymentMethodId);
}