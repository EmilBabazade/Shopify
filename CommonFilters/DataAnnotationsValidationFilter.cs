namespace Shopify.CommonFilters;

public sealed class DataAnnotationsValidationFilter<T> : IEndPointFilter<T> where T : class
{
    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext c, EndpointFilterDelegate next)
    {
        var model = c.Arguments.OfType<T>().FirstOrDefault();
        if (model is null) return Results.BadRequest("Invalid body.");

        var results = new List<ValidationResult>();
        var ctx = new ValidationContext(model);
        var ok = Validator.TryValidateObject(model, ctx, results, true);

        if (!ok)
        {
            var errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage!).ToArray());

            return Results.ValidationProblem(errors);
        }

        return await next(c);
    }

}