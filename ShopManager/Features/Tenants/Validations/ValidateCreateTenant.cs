using FluentValidation;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateCreateTenant : AbstractValidator<CreateTenantDto>
{
    private static readonly int MaxLengthForNames = 100;
    private static readonly int MaxLengthForEmails = 50;
    public ValidateCreateTenant()
    {
        RuleFor(x => x.Name)
            .MaximumLength(MaxLengthForNames).WithMessage($"Name must not exceed {MaxLengthForNames} characters")
            .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ContactName)
            .MaximumLength(MaxLengthForNames).WithMessage($"Contact Name must not exceed {MaxLengthForNames} characters")
            .NotEmpty().WithMessage("Contact Name is required");
        RuleFor(x => x.EmailAddress)
            .MaximumLength(MaxLengthForEmails).WithMessage($"Email Address must not exceed {MaxLengthForEmails} characters")
            .NotEmpty().WithMessage("Email Address is required");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(x => x.BillingAddress).NotEmpty().WithMessage("Billing Address is required");
        RuleFor(x => x.ActivationStatus).NotEmpty().WithMessage("Activation Status is required");
        RuleFor(x => x.PaymentStatus).NotEmpty().WithMessage("Payment Status is required");
        RuleFor(x => x.NextBillingDate).NotEmpty().WithMessage("Next Billing Date is required");
        RuleFor(x => x.SubscriptionStartDate).NotEmpty().WithMessage("Subscription Start Date is required");
        RuleFor(x => x.SubscriptionEndDate).NotEmpty().WithMessage("Subscription End Date is required");
    }
}
