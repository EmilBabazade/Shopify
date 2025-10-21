using CommonFilters;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.UsersAPI.Models;
using Shopify.UsersAPI.RequestBodies;
using Shopify.UsersAPI.Services;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Make scoped after adding permanent storage solution
builder.Services.AddSingleton<UsersService>();
// TODO: Make scoped after adding permanent storage solution
builder.Services.AddSingleton<JWTService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var group = app.MapGroup("/users");
group.AddEndpointFilter<ExceptionHandlerFilter>();

group.MapPost("/register", async (
        [FromBody] UserRequestBody userRequestBody,
        UsersService usersService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) => await usersService.CreateUser(userRequestBody, cancellationToken))
    .AddEndpointFilter<DataAnnotationsValidationFilter<UserRequestBody>>();

group.MapPost("/login", async (
        [FromBody] UserRequestBody userRequestBody,
        UsersService usersService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) => await usersService.Login(userRequestBody, cancellationToken))
    .AddEndpointFilter<DataAnnotationsValidationFilter<UserRequestBody>>();

app.Run();
