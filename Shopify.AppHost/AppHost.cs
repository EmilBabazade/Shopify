var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Shopify_UsersAPI>("shopify-usersapi");

builder.Build().Run();
