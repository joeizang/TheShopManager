using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.DomainModels;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Services;

public class SubscriptionPlanService(ShopManagerBaseContext context) : ISubscriptionPlan
{
    public async Task<Result<SubscriptionPlanTypeDto>> CreateSubscriptionPlanType(CreateSubscriptionPlanTypeDto inputModel)
    {
        var subscriptionPlanType = inputModel.MapToSubscriptionPlanType();
        context.SubscriptionPlanTypes.Add(subscriptionPlanType);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<SubscriptionPlanTypeDto>(subscriptionPlanType.MapToSubscriptionPlanTypeDto());
    }

    public async Task<Result<SubscriptionPlanDto>> CreateSubscriptionPlan(CreateSubscriptionPlanDto inputModel)
    {
        //You have to confirm that payment was successful before this happens.
        var subscriptionPlan = new SubscriptionPlan
        {
            SubscriptionPlanTypeId = inputModel.SubscriptionPlanTypeId,
            TenantId = inputModel.TenantId
        };
        context.SubscriptionPlans.Add(subscriptionPlan);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<SubscriptionPlanDto>(subscriptionPlan.MapSubscriptionPlanToDto());
    }

    public async Task<Result<SubscriptionPlanDto>> UpdateSubscriptionPlan(UpdateSubscriptionPlanDto inputModel)
    {
        //Updating a subscription plan is possible when the current plan is expired or
        //when the tenant wants to move up or down the plans. So if a current plan is active
        // then the tenant should be notified that they change will take effect when the current plan expires.
        var currentPlan = await context.SubscriptionPlans.FindAsync(inputModel.SubscriptionPlanId).ConfigureAwait(false);
        if (currentPlan is null)
        {
            return new Result<SubscriptionPlanDto>();
        }

        if (currentPlan.TenantId == inputModel.TenantId && 
            currentPlan.SubscriptionPlanTypeId != inputModel.SubscriptionPlanTypeId)
        {
            currentPlan.SubscriptionPlanTypeId = inputModel.SubscriptionPlanTypeId;
            context.Entry(currentPlan).State = EntityState.Modified;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new Result<SubscriptionPlanDto>(currentPlan.MapSubscriptionPlanToDto());
        }
        else
        {
            return new Result<SubscriptionPlanDto>();
        }
    }

    public async Task<Result<IResult>> DeleteSubscriptionPlan(Guid subscriptionPlanId)
    {
        var plan = await context.SubscriptionPlans.FindAsync(subscriptionPlanId).ConfigureAwait(false);
        if (plan is null)
        {
            return new Result<IResult>(TypedResults.NotFound());
        }
        plan.IsDeleted = true;
        context.Entry(plan).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<IResult>(TypedResults.NoContent());
    }

    public async Task<Result<IResult>> DeleteSubscriptionPlanType(Guid subscriptionPlanTypeId)
    {
        var plan = await context.SubscriptionPlanTypes.FindAsync(subscriptionPlanTypeId).ConfigureAwait(false);
        if (plan is null)
        {
            return new Result<IResult>(TypedResults.NotFound());
        }
        plan.IsDeleted = true;
        context.Entry(plan).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return new Result<IResult>(TypedResults.NoContent());
    }
}