var builder = WebApplication.CreateBuilder(args);

// Add services to the container (configure the services
// that the application will use e.g dependency injection,
// configuring services like logging and adding middlewares).

builder.Services
    .AddBasketModuleServices(builder.Configuration)
    .AddCatalogModuleServices(builder.Configuration)
    .AddOrderingModuleSevices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline (this is where
// we add middleware components in order to handle
// requests and responses).

app.Run();
