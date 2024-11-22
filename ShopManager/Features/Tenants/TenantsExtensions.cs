using NodaTime;
using NodaTime.Text;
using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants;
using ShopManager.Features.Tenants.DomainModels;

public static class TenantsExtensions
{
    public static Tenant MapToTenant(this CreateTenantDto dto)
    {
        if (InstantPattern.ExtendedIso.Parse(dto.SubscriptionEndDate) 
                is not { Success: true, Value: var parsedEndDate } 
            || InstantPattern.ExtendedIso.Parse(dto.SubscriptionStartDate) is not { Success: true, Value: var parsedStartDate }
            || InstantPattern.ExtendedIso.Parse(dto.NextBillingDate) is not { Success: true, Value: var parsedNextBillingDate })
        {
            throw new FormatException("Invalid dates passed");
        }
        else
        {
            
            return new Tenant
            {
                Name = dto.Name,
                ContactName = dto.ContactName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                BillingAddress = dto.BillingAddress,
                ActivationStatus = dto.ActivationStatus,
                NextBillingDate = parsedNextBillingDate,
                SubscriptionStartDate = parsedStartDate,
                SubscriptionEndDate = parsedEndDate,
                PaymentStatus = PaymentStatus.UNINITIALIZED
            };
        }
    }
    
    public static TenantDto MapToTenantDto(this Tenant tenant)
    {
        return new TenantDto(Name: tenant.Name, ContactName: tenant.ContactName, EmailAddress: tenant.EmailAddress,
            PhoneNumber: tenant.PhoneNumber, Address: tenant.Address, BillingAddress: tenant.BillingAddress,
            ActivationStatus: tenant.ActivationStatus, NextBillingDate: tenant.NextBillingDate,
            SubscriptionStartDate: tenant.SubscriptionStartDate, SubscriptionEndDate: tenant.SubscriptionEndDate,
            PaymentStatus: tenant.PaymentStatus);
    }
}