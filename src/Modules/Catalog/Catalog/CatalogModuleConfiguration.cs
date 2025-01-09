namespace Catalog
{
    public static class CatalogModuleConfiguration
    {
        public static IApplicationBuilder UseCatalogModuleConfigurations(this IApplicationBuilder applicationBuilder)
        {
            // Configure the HTTP request pipeline
            // applicationBuilder
            //      .UseApplicationServices()
            //      .UseInfrastructureServices()
            //      .UseApiServices();

            // 1.- Use Api Endpoint Services

            // 2.- Use Application Use Case Services

            // 3.- Use Data - Infrastructure Services

            applicationBuilder.UseMigration<CatalogDbContext>();

            return applicationBuilder;
        }
    }
}
