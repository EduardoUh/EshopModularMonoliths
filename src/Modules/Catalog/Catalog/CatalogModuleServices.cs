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

            // data - infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<CatalogDbContext>(options =>
                options.UseNpgsql(connectionString ?? throw new Exception("Database connection string not found")));

            return services;
        }
    }
}
