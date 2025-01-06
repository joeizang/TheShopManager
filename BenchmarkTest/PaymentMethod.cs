namespace ShopManager.DomainModels;

public enum PaymentMethod
{
    NOT_SET = 10,
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
    UNINITIALIZED = 1,
    PENDING,
    SUCCESSFUL,
    FAILED
}