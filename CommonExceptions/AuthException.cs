namespace Shopify.CommonExceptions;

public class AuthException : Exception
{
    public AuthException()
    {
    }

    public AuthException(string? message) : base(message)
    {
    }
}