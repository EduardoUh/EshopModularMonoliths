var builder = WebApplication.CreateBuilder(args);

// Add services to the container (configure the services
// that the application will use e.g dependency injection,
// configuring services like logging and adding middlewares).

builder.Services.AddCarterWithAssemblies(typeof(CatalogModuleServices).Assembly);

builder.Services
    .AddBasketModuleServices(builder.Configuration)
    .AddCatalogModuleServices(builder.Configuration)
    .AddOrderingModuleSevices(builder.Configuration);

// Injection the custom exception handler class
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline (this is where
// we add middleware components in order to handle
// requests and responses).

app.MapCarter();

app
    .UseBasketModuleConfigurations()
    .UseCatalogModuleConfigurations()
    .UseOrderingModuleConfigurations();

// Making the app use an exception handler, by letting it empty it will use
// our custom exception handler
app.UseExceptionHandler(options => { });

app.Run();
