using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record TenantPaymentDto();


public record CreateTenantPaymentDto();


public record UpdateTenantPaymentDto();


public record TenantPaymentMethodDto(Guid TenantId, string PaymentDetails, 
    PaymentMethod PaymentMethod, bool IsDefaultPaymentMethod);


public record CreateTenantPaymentMethodDto(Guid TenantId, string PaymentDetails, 
    PaymentMethod PaymentMethod);


public record UpdateTenantPaymentMethodDto(Guid PaymentMethodId, string PaymentDetails, 
    PaymentMethod PaymentMethod, bool IsDefaultPaymentMethod, Guid TenantId);