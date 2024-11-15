namespace ShopManager.DomainModels;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Extensions;
    public class Product : BaseDomainModel
    {
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; } = new();

        public string Sku { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public Money CostPrice { get; set; } = new(Currency.NGN, 0m);

        public Money SellingPrice { get; set; } = new(Currency.NGN, 0m);

        public bool IsFairlyUsed { get; set; }

        public Guid CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new();

        public Guid? FairlyUsedItemId { get; set; }

        public FairlyUsedItem? FairlyUsedItem { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = new();
    }

    public class Category : BaseDomainModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;
    }

    public class Customer : BaseDomainModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public List<Sale> Sales { get; set; } = new();

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;
    }

    public class Supplier : BaseDomainModel
    {
        public string SupplierName { get; set; } = string.Empty;

        public string SupplierAddress { get; set; } = string.Empty;

        public string SupplierPhoneNumber { get; set; } = string.Empty;

        public string SupplierEmailAddress { get; set; } = string.Empty;

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;

        public List<Product> Products { get; set; } = new();
    }

    public class Inventory : BaseDomainModel
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; } = default!;

        public double QuantityInStock { get; set; }

        public double ReOrderLevel { get; set; }

        public double ReOrderQuantity { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;
    }

    public class Sale : BaseDomainModel
    {
        public Guid SaleId { get; set; }

        public ZonedDateTime SaleDate { get; set; }

        public Money TotalAmount { get; set; } = new(Currency.NGN, 0m);

        public Guid? CustomerId { get; set; }

        public Customer Customer { get; set; } = default!;

        public string SalesPersonId { get; set; } = string.Empty;

        public ApplicationUser SalesPerson { get; set; } = default!;

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;

        public List<SaleItem> SaleItems { get; set; } = new();

        public List<Payment> Payments { get; set; } = new();
    }

public record Money(Currency currency, decimal amount)
{
    public Currency Currency { get; } = currency;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; } = amount;
}

public enum Currency
    {
        USD,
        NGN,
        GBP,
        EUR,
        JPY,
        CNY,
        INR,
        AUD,
        CAD,
        ZAR,
        GHS,
        KES,
        UGX,
        RWF,
        XAF,
        XOF,
        XPF,
        GNF,
        GMD,
        LRD,
        SLL,
        MWK,
        MZN,
        ZMW
    }

    public class SaleItem : BaseDomainModel
    {
        public Guid SaleId { get; set; }

        public Sale Sale { get; set; } = default!;

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = default!;

        public double QuantitySold { get; set; }

        public Money UnitPrice { get; set; } = new(Currency.NGN, 0m);

        public Money TotalAmount { get; set; } = new(Currency.NGN, 0m);

    }

    public class Payment : BaseDomainModel
    {
        public Guid SalesId { get; set; }

        public Sale Sale { get; set; } = default!;

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;

        public Money AmountPaid { get; set; } = new(Currency.NGN, 0m);

        public ZonedDateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string PaymentReference { get; set; } = string.Empty;

    }

    public enum PaymentMethod
    {
        CASH,
        POS,
        BANK_TRANSFER,
        USSD,
        CHEQUE,
        MOBILE_MONEY,
        CRYPTO_CURRENCY
    }

    public enum FairlyUsedItemCondition
    {
        EXCELLENT,
        VERY_GOOD,
        GOOD,
        FAIR,
        POOR
    }

    public class FairlyUsedItem : BaseDomainModel
    {
        public Guid PurchaseId { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = default!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = default!;

        public ZonedDateTime DateBought { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = default!;

        public FairlyUsedItemCondition Condition { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;

        public string ItemName { get; set; } = string.Empty;

        public string ItemDescription { get; set; } = string.Empty;

        public string ItemDetails { get; set; } = string.Empty;

        public Money Price { get; set; } = new(Currency.NGN, 0m);

    }

    public class Shop : BaseDomainModel
    {
        public string ShopName { get; set; } = string.Empty;

        public string ShopAddress { get; set; } = string.Empty;

        public string ShopPhoneNumber { get; set; } = string.Empty;

        public string ShopEmailAddress { get; set; } = string.Empty;

        public string ShopLogo { get; set; } = string.Empty;

        public string ShopDescription { get; set; } = string.Empty;

        public string CacRegistrationNumber { get; set; } = string.Empty;

        public string TaxIdentificationNUmber { get; set; } = string.Empty;

        public bool Status { get; set; } = false;
    }
