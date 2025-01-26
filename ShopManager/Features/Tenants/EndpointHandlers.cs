using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using ShopManager.Core;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Services;

namespace ShopManager.Features.Tenants;

public static class EndpointHandlers
{
    public static async Task<IResult> CreateTenant([FromServices] ITenantCommandService command, 
        [FromBody] CreateTenantDto inputModel)
    {
            var result = await command.CreateTenant(inputModel).ConfigureAwait(false);
            return result.Match<IResult>(r => TypedResults.Created<TenantDto>("", r),
                error => TypedResults.InternalServerError());
    }

    public static async Task<IResult> GetTenantByIdAsync(Guid tenantId, 
        [FromServices] ShopManagerBaseContext context)
    {
        var tenant = await TenantsQueryService.GetTenantById(context, tenantId);
        return tenant is not null ? TypedResults.Ok(tenant) : TypedResults.NotFound();
    }
    
    public static async Task<IResult> GetTenants([FromServices] ShopManagerBaseContext context)
    {
        var result = new List<TenantDto>();
        await foreach (var tenant in TenantsQueryService.GetTenantsAsync(context))
        {
            result.Add(tenant);
        }
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetCursoredTenants([FromQuery] string cursor, [FromServices] ShopManagerBaseContext context)
    {
        if (InstantPattern.ExtendedIso.Parse(cursor.ToString()) is not { Success: true, Value: var parsedCursor })
        {
            return Results.BadRequest("Invalid cursor");
        }
        var result = new List<TenantDto>();
        await foreach(var tenant in TenantsQueryService.GetCursoredTenantsAsync(context, parsedCursor))
        {
            result.Add(tenant);
        }
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> DeleteTenant(Guid tenantId, [FromServices] ITenantCommandService command)
    {
        var result = await command.DeleteTenant(tenantId).ConfigureAwait(false);
        return result;
    }

    public static async Task<IResult> GetSubscriptionPlansForTenant(Guid tenantId,
        [FromServices] ShopManagerBaseContext context)
    {
        List<SubscriptionPlanDto> result = [];
        await foreach (var plan in TenantsQueryService.GetSubscriptionPlanForTenantAsync(context, tenantId))
        {
            result.Add(plan);
        }
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> CreateSubscriptionPlan([FromBody] CreateSubscriptionPlanDto inputModel,
        [FromServices] ISubscriptionPlan service
        )
    {
        var result = await service.CreateSubscriptionPlan(inputModel);
        return result.Match<IResult>(
            r => TypedResults.Created<SubscriptionPlanDto>("", r),
            error => TypedResults.InternalServerError());
    }

    public static async Task<IResult> GetSubscriptionPlanById(Guid subId, [FromServices] ShopManagerBaseContext context)
    {
        var result = await TenantsQueryService.GetSubscriptionPlanById(context, subId);
        return result.Match<IResult>(
            TypedResults.Ok,
            error => TypedResults.NotFound());
    }

    public static async Task<IResult> DeleteSubscriptionPlan(Guid subscriptionPlanId, [FromServices] ISubscriptionPlan service)
    {
        var result = await service.DeleteSubscriptionPlan(subscriptionPlanId);
        return result.Match<IResult>(
            r => TypedResults.NoContent(),
            error => TypedResults.NotFound());
    }

    public static async Task<IResult> GetSubscriptionPlans([FromServices] ShopManagerBaseContext context)
    {
        List<SubscriptionPlanDto> result = [];
        await foreach (var plan in TenantsQueryService.GetSubscriptionPlansAsync(context))
        {
            result.Add(plan);
        }

        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetCursoredSubscriptionPlans(string cursor, [FromServices] ShopManagerBaseContext context)
    {
        List<SubscriptionPlanDto> result = [];
        await foreach(var plan in TenantsQueryService.GetCursoredSubscriptionPlansAsync(context, cursor.ToInstantDate()))
        {
            result.Add(plan);
        }
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetSubscriptionPlanTypes([FromServices] ShopManagerBaseContext context)
    {
        List<SubscriptionPlanTypeDto> result = [];
        await foreach (var planType in TenantsQueryService.GetSubscriptionPlanTypesAsync(context))
        {
            result.Add(planType);
        }

        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetCursoredSubscriptionPlanTypes(string cursor, [FromServices] ShopManagerBaseContext context)
    {
        List<SubscriptionPlanTypeDto> result = [];
        await foreach (var planType in TenantsQueryService.GetCursoredSubscriptionPlanTypesAsync(context, cursor.ToInstantDate()))
        {
            result.Add(planType);
        }

        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetSubscriptionPlanTypeById(Guid subTypeId, ShopManagerBaseContext context)
    {
        var result = await TenantsQueryService.GetSubscriptionPlanTypesById(context, subTypeId);
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public static async Task<IResult> CreateSubscriptionPlanType([FromBody] CreateSubscriptionPlanTypeDto inputModel,
        [FromServices] ISubscriptionPlan service)
    {
        var result = await service.CreateSubscriptionPlanType(inputModel);
        var returned = result.Match<IResult>(
            r => TypedResults.Created<SubscriptionPlanTypeDto>("", r),
            error => TypedResults.BadRequest("There was an error creating your subscription plan type!"));
        return returned is not null ? 
            TypedResults.Created<IResult>("", returned) : 
            TypedResults.BadRequest("Your subscription plan could not be created!");
    }

    public static async Task<IResult> DeleteSubscriptionPlanType(Guid subscriptionPlanTypeId, [FromServices] ISubscriptionPlan service)
    {
        var result = await service.DeleteSubscriptionPlanType(subscriptionPlanTypeId);
        return result.Match<IResult>(
            r => TypedResults.NoContent(),
            error => TypedResults.NotFound());
    }
    
    public static async Task<IResult> CreateTenantPaymentMethod([FromBody] CreateTenantPaymentMethodDto inputModel,
        [FromServices] ITenantPaymentMethodCommandService service)
    {
        var result = await service.CreateTenantPaymentMethodAsync(inputModel);
        return result.Match<IResult>(
            r => TypedResults.Created<TenantPaymentMethodDto>("", r),
            error => TypedResults.InternalServerError());
    }
    
    public static async Task<IResult> UpdateTenantPaymentMethod(Guid paymentMethodId, [FromBody] UpdateTenantPaymentMethodDto inputModel,
        [FromServices] ITenantPaymentMethodCommandService service)
    {
        var result = await service.UpdateTenantPaymentMethodAsync(inputModel);
        return result.Match<IResult>(
            TypedResults.Ok,
             TypedResults.NotFound()
        );
    }

    
    public static IResult GetTenantPaymentMethodByTenantId(Guid tenantId,
        [FromServices] ShopManagerBaseContext context)
    {
        var result = TenantsQueryService
            .GetTenantPaymentMethodByTenantId(context, tenantId);
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public static IResult GetTenantPaymentsByTenantId(Guid tenantId,
        [FromServices] ShopManagerBaseContext context)
    {
        var result = TenantsQueryService.GetTenantPaymentsByTenantId(context, tenantId);
        return TypedResults.Ok(result);
    }
    

    public static async Task<IResult> DeleteTenantPaymentMethod(Guid paymentMethodId,
        [FromServices] ITenantPaymentMethodCommandService service)
    {
        var result = await service.DeleteTenantPaymentMethodAsync(paymentMethodId);
        return result.Match<IResult>( r => TypedResults.NoContent(),
            TypedResults.NotFound
        );
    }
    
    public static IResult GetTenantPaymentMethods(Guid tenantId, [FromServices] ShopManagerBaseContext context)
    {
        List<TenantPaymentMethodDto> result = [];
        var results = TenantsQueryService.GetTenantPaymentMethods(context, tenantId);
        
        return TypedResults.Ok(result);
    }
}