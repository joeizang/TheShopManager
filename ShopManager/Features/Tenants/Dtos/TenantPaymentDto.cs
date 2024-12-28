using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record TenantPaymentDto();


public record CreateTenantPaymentDto(
    Guid TenantId, Guid TenantInvoiceId, Guid PaymentMethodId, string PaymentReference, 
    string Description, decimal AmountPaid, PaymentStatus Status);


public record UpdateTenantPaymentDto(Guid TenantPaymentId, Guid TenantId, Guid TenantInvoiceId, 
    Guid PaymentMethodId, string PaymentReference, string Description, decimal AmountPaid, 
    PaymentStatus Status);


public record TenantPaymentMethodDto(Guid TenantId, string PaymentDetails, 
    PaymentMethod PaymentMethod, bool IsDefaultPaymentMethod);


public record CreateTenantPaymentMethodDto(Guid TenantId, string PaymentDetails, 
    PaymentMethod PaymentMethod);


public record UpdateTenantPaymentMethodDto(Guid PaymentMethodId, string PaymentDetails, 
    PaymentMethod PaymentMethod, bool IsDefaultPaymentMethod, Guid TenantId);