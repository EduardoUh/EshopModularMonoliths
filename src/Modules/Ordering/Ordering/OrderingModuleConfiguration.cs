using Microsoft.AspNetCore.Builder;

namespace Ordering
{
    public static class OrderingModuleConfiguration
    {
        public static IApplicationBuilder UseOrderingModuleConfigurations(this IApplicationBuilder applicationBuilder)
        {
            // Configura the HTTP request pipeline
            // applicationBuilder
            //      .UseApplicationServices()
            //      .UseInfrastructureServices()
            //      .UseApiServices();

            return applicationBuilder;
        }
    }
}
