namespace Shopify.CommonExceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string? message) : base(message)
    {
    }
}

public class NotFoundException : Exception
{
    public NotFoundException()
    {
        
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}