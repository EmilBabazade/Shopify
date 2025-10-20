namespace Shopify.CommonExceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException()
    {
    }

    public AuthorizationException(string? message) : base(message)
    {
    }
}