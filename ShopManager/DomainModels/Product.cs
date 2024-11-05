namespace ShopManager.DomainModels
{
    public class Product : BaseDomainModel
    {
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; } = new();

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = new();

        public Guid FairlyUsedId { get; set; }

        public FairlyUsed FairlyUsed { get; set; } = new();
    }

    public class Category : BaseDomainModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;
    }

    public class Customer : BaseDomainModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }

    public class Supplier : BaseDomainModel
    {

    }

    public class Inventory : BaseDomainModel
    {
        public Guid ProductId { get; set; }

        public float QuantityInStock { get; set; }

        public float ReOrderLevel { get; set; }

        public float ReOrderQuantity { get; set; }
    }

    public class Sales : BaseDomainModel
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

    public class SaleItems : BaseDomainModel
    {
        public Guid SalesId { get; set; }

    }

    public class Payments : BaseDomainModel
    {
        public Guid SalesId { get; set; }

    }

    public class FairlyUsed : BaseDomainModel
    {
        
    }

    public class FairlyUsedItems : BaseDomainModel
    {

    }
}
