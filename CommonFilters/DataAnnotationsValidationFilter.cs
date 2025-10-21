using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CommonFilters;

public sealed class DataAnnotationsValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.Arguments.OfType<T>().FirstOrDefault();
        if (model is null)
            return Results.BadRequest("Invalid body.");

        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        var isValid = Validator.TryValidateObject(model, validationContext, results, true);

        if (!isValid)
        {
            var errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage!).ToArray());

            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}
