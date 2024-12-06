using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.DomainModels;

public record Money(Currency currency, decimal amount)
{
    public Currency Currency { get; } = currency;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; } = amount;
    
    public static Money operator +(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot add money of different currencies");
        }

        return new Money(money1.Currency, money1.Amount + money2.Amount);
    }
    
    public static Money operator -(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot subtract money of different currencies");
        }

        return new Money(money1.Currency, money1.Amount - money2.Amount);
    }
    
    public static Money operator *(Money money, decimal multiplier)
    {
        return new Money(money.Currency, money.Amount * multiplier);
    }
    
    public static Money operator /(Money money, decimal divisor)
    {
        return new Money(money.Currency, money.Amount / divisor);
    }
}