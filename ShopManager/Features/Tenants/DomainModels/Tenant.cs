using LanguageExt;
using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;
using Duration = NodaTime.Duration;

namespace ShopManager.Features.Tenants.DomainModels;

public class Tenant : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string BillingAddress { get; set; } = string.Empty;

    public ActivationStatus ActivationStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public Instant NextBillingDate { get; private set; }

    public Instant SubscriptionStartDate { get; set; }

    public Instant SubscriptionEndDate { get; set; }
    
    public ICollection<TenantPaymentMethod> PaymentMethods { get; set; } = [];

    public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = [];

    public ICollection<Shop> Shops { get; set; } = [];

    public ICollection<TenantInvoice> TenantInvoices { get; set; } = [];

    public ICollection<TenantPayment> TenantPayments { get; set; } = [];

    public Tenant(Guid subscriptionPlanTypeId)
    {
        // basically, there cannot be a tenant without a subscription plan
        SubscriptionPlans.Add(new SubscriptionPlan
        {
            SubscriptionPlanTypeId = subscriptionPlanTypeId,
            Status = ActivationStatus.INACTIVE,
            TenantId = Id
        });
    }

    private Tenant()
    {
        
    }

    public void CalculateEndAndNextDates(Guid subscriptionPlanId)
    {
        // tenants can only have one subscription plan at a time
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
    
    
    // what does settling an invoice mean?
    public Option<TenantPayment> SettleLatestInvoice()
    {
        //get the latest invoice
        var latestInvoice = TenantInvoices
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();
        
        if(latestInvoice is null)
        {
            return Option<TenantPayment>.None; //there is nothing to pay
        }

        var paymentMethodId = PaymentMethods
            .FirstOrDefault(x => x.IsDefaultPaymentMethod)!.Id;
        var newPayment = new TenantPayment(paymentMethodId)
        {
            AmountPaid = latestInvoice.AmountDue,
            PaymentDate = ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now).ToInstant(),
            PaymentReference = Guid.NewGuid().ToString(),
            Description = "Payment for invoice " + latestInvoice.InvoiceReference,
            Status = PaymentStatus.PENDING,
            TenantId = Id,
            TenantInvoiceId = latestInvoice.Id
        };
        TenantPayments.Add(newPayment);
        return Option<TenantPayment>.Some(newPayment);
    }
}