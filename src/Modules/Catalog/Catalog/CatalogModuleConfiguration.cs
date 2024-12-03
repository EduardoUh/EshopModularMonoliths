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

            return applicationBuilder;
        }
    }
}
