namespace ShopManager.DomainModels
{
    public class Product
    {
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; } = new();

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = new();

        public Guid FairlyUsedId { get; set; }

        public FairlyUsed FairlyUsed { get; set; } = new();
    }

    public class Category
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;
    }

    public class Customer
    {
        
    }

    public class Supplier
    {

    }

    public class Inventory
    {
        public Guid ProductId { get; set; }

        public float QuantityInStock { get; set; }

        public float ReOrderLevel { get; set; }

        public float ReOrderQuantity { get; set; }
    }

    public class Sales
    {
        public Guid SaleId { get; set; }

        public required Money TotalAmount { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid SalesPersonId { get; set; }

        public ApplicationUser SalesPerson { get; set; } = new();
    }

    public record Money
    {
        public decimal Amount { get; init; }
        public Currency Currency { get; init; }
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

    public class SaleItems
    {
        public Guid SalesId { get; set; }

    }

    public class Payments
    {
        public Guid SalesId { get; set; }

    }

    public class FairlyUsed
    {
        
    }

    public class FairlyUsedItems
    {

    }
}
