namespace ShopManager.CustomExceptions;

public class NotFoundException(string message) : Exception(message)
{
    
}