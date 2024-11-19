namespace ShopManager.DomainModels;

public enum PaymentMethod
{
    NOT_SET,
    CASH,
    POS,
    BANK_TRANSFER,
    USSD,
    CHEQUE,
    MOBILE_MONEY,
    CRYPTO_CURRENCY
}

public enum PaymentStatus
{
    PENDING,
    SUCCESSFUL,
    FAILED,
    UNINITIALIZED
}