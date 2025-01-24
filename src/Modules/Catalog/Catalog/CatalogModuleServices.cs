namespace Catalog
{
    public static class CatalogModuleServices
    {
        public static IServiceCollection AddCatalogModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container
            // services
            //      .AddApplicationServices()
            //      .AddInfrastructureServices(configuration)
            //      .AddApiServices(configuration)

            // Api endpoint services

            // Application use case services
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // data - infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
            {
                var interceptors = serviceProvider.GetServices<ISaveChangesInterceptor>();

                options.AddInterceptors(interceptors);

                options.UseNpgsql(connectionString ?? throw new Exception("Database connection string not found"));
            });

            services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }
    }
}
