using System.Runtime.Serialization;

namespace Shopify.UsersAPI.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string? message) : base(message)
    {
    }
}
