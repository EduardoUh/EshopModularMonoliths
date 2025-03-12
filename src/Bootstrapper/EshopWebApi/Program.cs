var builder = WebApplication.CreateBuilder(args);

// Add services to the container (configure the services
// that the application will use e.g dependency injection,
// configuring services like logging and adding middlewares).

builder.Services.AddCarterWithAssemblies(typeof(CatalogModuleServices).Assembly);

builder.Services
    .AddBasketModuleServices(builder.Configuration)
    .AddCatalogModuleServices(builder.Configuration)
    .AddOrderingModuleSevices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline (this is where
// we add middleware components in order to handle
// requests and responses).

app.MapCarter();

app
    .UseBasketModuleConfigurations()
    .UseCatalogModuleConfigurations()
    .UseOrderingModuleConfigurations();

app.UseExceptionHandler(exceptionHandler =>
{
    exceptionHandler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is null) return;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        logger.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.Run();
