using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record SubscriptionPlanTypeDto(string Name, string Description, decimal Price, Currency PaymentCurrency, 
    string Features, decimal Discount);