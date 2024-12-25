using NodaTime;
using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record CreateTenantInvoiceDto(string DueDate, decimal Amount, Currency Currency,
    string InvoiceReference, string Description, InvoiceStatus InvoiceStatus, Guid TenantId);
    
public record TenantInvoiceDto(Guid TenantInvoiceId, string DueDate, string InvoiceDate,
    decimal Amount, Currency Currency,
    string InvoiceReference, string Description, InvoiceStatus InvoiceStatus, Guid TenantId);
    
    
public record UpdateTenantInvoiceDto(Guid TenantInvoiceId, string DueDate, string InvoiceDate,
    decimal Amount, Currency Currency,
    string InvoiceReference, string Description, InvoiceStatus InvoiceStatus, Guid TenantId);    