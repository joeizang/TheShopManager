using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.DomainModels;

public record Money(Currency currency, decimal amount)
{
    public Currency Currency { get; } = currency;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; } = amount;
}