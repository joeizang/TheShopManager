namespace ShopManager.CustomExceptions;

public class NoPaymentMethodException(string message) : Exception(message);