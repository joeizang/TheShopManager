using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants.Services;

public class SubscriptionPlanService(ShopManagerBaseContext context) : ISubscriptionPlan
{
    public async Task<SubscriptionPlanTypeDto> CreateSubscriptionPlanType(CreateSubscriptionPlanTypeDto inputModel)
    {
        var subscriptionPlanType = inputModel.MapToSubscriptionPlanType();
        context.SubscriptionPlanTypes.Add(subscriptionPlanType);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return subscriptionPlanType.MapToSubscriptionPlanTypeDto();
    }

    public async Task<SubscriptionPlanDto> CreateSubscriptionPlan(CreateSubscriptionPlanDto inputModel)
    {
        //You have to confirm that payment was successful before this happens.
        var subscriptionPlan = new SubscriptionPlan
        {
            SubscriptionPlanTypeId = inputModel.SubscriptionPlanTypeId,
            TenantId = inputModel.TenantId
        };
        context.SubscriptionPlans.Add(subscriptionPlan);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return subscriptionPlan.MapSubscriptionPlanToDto();
    }

    public async Task<SubscriptionPlanDto> UpdateSubscriptionPlan(UpdateSubscriptionPlanDto inputModel)
    {
        //Updating a subscription plan is possible when the current plan is expired or
        //when the tenant wants to move up or down the plans. So if a current plan is active
        // then the tenant should be notified that they change will take effect when the current plan expires.
        var currentPlan = await context.SubscriptionPlans.FindAsync(inputModel.SubscriptionPlanId).ConfigureAwait(false);
        if (currentPlan is null)
        {
            return null;
        }

        if (currentPlan.TenantId == inputModel.TenantId && 
            currentPlan.SubscriptionPlanTypeId != inputModel.SubscriptionPlanTypeId)
        {
            currentPlan.SubscriptionPlanTypeId = inputModel.SubscriptionPlanTypeId;
            context.Entry(currentPlan).State = EntityState.Modified;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return currentPlan.MapSubscriptionPlanToDto();
        }
        else
        {
            return null;
        }
    }

    public async Task<IResult> DeleteSubscriptionPlan(Guid subscriptionPlanId)
    {
        var plan = await context.SubscriptionPlans.FindAsync(subscriptionPlanId).ConfigureAwait(false);
        if (plan is null)
        {
            return TypedResults.NotFound();
        }
        plan.IsDeleted = true;
        context.Entry(plan).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return TypedResults.NoContent();
    }

    public async Task<IResult> DeleteSubscriptionPlanType(Guid subscriptionPlanTypeId)
    {
        var plan = await context.SubscriptionPlanTypes.FindAsync(subscriptionPlanTypeId).ConfigureAwait(false);
        if (plan is null)
        {
            return TypedResults.NotFound();
        }
        plan.IsDeleted = true;
        context.Entry(plan).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
        return TypedResults.NoContent();
    }
}