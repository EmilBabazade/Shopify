using System.ComponentModel.DataAnnotations;

namespace Shopify.UsersAPI.RequestBodies;

public record class UserRequestBody(
    [property: Required(ErrorMessage = "Email is requierd")]
    [property: EmailAddress(ErrorMessage = "Invalid Email Address")]
    string Email,
    [property: Required(ErrorMessage = "Password is required")]
    [property: RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include a letter, a number, and a symbol.")]
    string Password
);