using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Shopify.UsersAPI.Models;
using Shopify.UsersAPI.RequestBodies;
using Shopify.UsersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<JWTService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: validation error handling common filter
var group = app.MapGroup("/users");
group
    .MapPost("/register", async (
        [FromBody] UserRequestBody userRequestBody,
        UsersService usersService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) => usersService.CreateUser(userRequestBody, cancellationToken))
    .AddEndpointFilter<DataAnnotationsValidationFilter>();

app.Run();
