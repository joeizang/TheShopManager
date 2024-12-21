using LanguageExt.Common;
using NodaTime;
using NodaTime.Text;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;
using ShopManager.Features.Tenants.Dtos;

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
                // NextBillingDate = parsedNextBillingDate,
                SubscriptionStartDate = parsedStartDate,
                SubscriptionEndDate = parsedEndDate,
                PaymentStatus = PaymentStatus.UNINITIALIZED
            };
        }
    }

    public static Tenant MapToTenant(this UpdateTenantDto dto)
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
                Id = dto.TenantId,
                Name = dto.Name,
                ContactName = dto.ContactName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                BillingAddress = dto.BillingAddress,
                ActivationStatus = dto.ActivationStatus,
                // NextBillingDate = parsedNextBillingDate,
                SubscriptionStartDate = parsedStartDate,
                SubscriptionEndDate = parsedEndDate,
                PaymentStatus = dto.PaymentStatus
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

    public static SubscriptionPlanType MapToSubscriptionPlanType(this CreateSubscriptionPlanTypeDto dto)
    {
        // the currency should be fetched from the profile of every tenant.
            return new SubscriptionPlanType
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = new Money(Currency.NGN, dto.Price),
                Features = dto.Features,
                Discount = dto.Discount
            };
    }
    
    public static SubscriptionPlanTypeDto MapToSubscriptionPlanTypeDto(this SubscriptionPlanType dto)
    {
        return new SubscriptionPlanTypeDto(Name: dto.Name, Description: dto.Description,
            Price: dto.Price.Amount, Currency.NGN, Features: dto.Features, Discount: dto.Discount);
    }
    
    public static object MapToSubscriptionPlanDto(this SubscriptionPlan subscriptionPlan)
    {
        // Next billing date has to be calculated.
        return new { };
        // return new SubscriptionPlanDto(TenantId: subscriptionPlan.TenantId, SubscriptionPlanTypeId: subscriptionPlan.SubscriptionPlanTypeId,
        //     SubscriptionPlanTypeName: subscriptionPlan.SubscriptionPlanType.Name, Price: subscriptionPlan.SubscriptionPlanType.Price,
        //     BillingCycle: subscriptionPlan.SubscriptionPlanType.BillingCycle, 
        //     NextBillingDate: subscriptionPlan.NextBillingDate, Status: subscriptionPlan.Status);
    }
    
    public static SubscriptionPlan MapToSubscriptionPlan(this CreateSubscriptionPlanDto dto)
    {
        return new SubscriptionPlan
        {
            TenantId = dto.TenantId,
            SubscriptionPlanTypeId = dto.SubscriptionPlanTypeId
        };
    }
    
    public static SubscriptionPlanDto MapSubscriptionPlanToDto(this SubscriptionPlan subscriptionPlan)
    {
        return new SubscriptionPlanDto(TenantId: subscriptionPlan.TenantId, 
            SubscriptionPlanTypeId: subscriptionPlan.SubscriptionPlanTypeId,
            SubscriptionPlanTypeName: subscriptionPlan.SubscriptionPlanType.Name, 
            Price: subscriptionPlan.SubscriptionPlanType.Price.Amount,
            PaymentCurrency: subscriptionPlan.SubscriptionPlanType.Price.Currency,
            BillingCycle: subscriptionPlan.SubscriptionPlanType.BillingCycle, Status: subscriptionPlan.Status,
            TenantName: subscriptionPlan.Tenant.Name);
    }
    
    public static Result<SubscriptionPlanDto?> ProjectToSubscriptionPlanDtoResult(this SubscriptionPlanDto? subscriptionPlan)
    {
        return new Result<SubscriptionPlanDto?>(subscriptionPlan);
    }
}