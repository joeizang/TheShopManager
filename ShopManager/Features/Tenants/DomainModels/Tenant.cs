using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class Tenant : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string BillingAddress { get; set; } = string.Empty;

    public bool ActivationStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public Instant NextBillingDate { get; private set; }

    public Instant SubscriptionStartDate { get; set; }

    public Instant SubscriptionEndDate { get; set; }
    
    public ICollection<TenantPaymentMethod> PaymentMethods { get; set; } = [];

    public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = [];

    public ICollection<Shop> Shops { get; set; } = [];

    public ICollection<TenantInvoice> TenantInvoices { get; set; } = [];

    public ICollection<TenantPayment> TenantPayments { get; set; } = [];

    public void CalculateEndAndNextDates(Guid subscriptionPlanId)
    {
        var subscriptionPlan = SubscriptionPlans.FirstOrDefault(x => x.Id == subscriptionPlanId);
        var currentMonth = ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now).Month;
        if (subscriptionPlan is null)
        {
            throw new InvalidOperationException("Subscription plan not found");
        }

        if(subscriptionPlan.SubscriptionPlanType.BillingCycle == BillingCycle.MONTHLY)
        {
            if (ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now).Calendar.IsLeapYear(DateTimeOffset.Now.Year))
            {
                if(ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now).Month == 2)
                {
                    SubscriptionEndDate = SubscriptionStartDate.Plus(Duration.FromDays(29));
                }
            }
            else
            {
                if(ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now).Month == 2)
                {
                    SubscriptionEndDate = SubscriptionStartDate.Plus(Duration.FromDays(28));
                }
            }
            
            if(currentMonth is 4 or 6 or 9 or 11)
            {
                SubscriptionEndDate = SubscriptionStartDate.Plus(Duration.FromDays(30));
            }
            else
            {
                SubscriptionEndDate = SubscriptionStartDate.Plus(Duration.FromDays(31));
            }

        }
        else if(subscriptionPlan.SubscriptionPlanType.BillingCycle == BillingCycle.YEARLY)
        {
            CalculateYearlyBillingCycle();
        }
        NextBillingDate = SubscriptionEndDate.Plus(Duration.FromDays(1));
    }

    private void CalculateYearlyBillingCycle()
    {
        throw new NotImplementedException();
    }
}