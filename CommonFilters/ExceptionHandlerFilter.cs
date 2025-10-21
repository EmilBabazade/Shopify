using Microsoft.AspNetCore.Http;
using Shopify.CommonExceptions;

namespace CommonFilters;

public sealed class ExceptionHandlerFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (BadRequestException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (AuthException)
        {
            return Results.Unauthorized();
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }
        catch
        {
            return Results.Problem();
        }
    }
}
