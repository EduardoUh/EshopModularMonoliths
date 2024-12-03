namespace Basket
{
    public static class BasketModuleConfiguration
    {
        public static IApplicationBuilder UseBasketModuleConfigurations(this IApplicationBuilder applicationBuilder)
        {
            // Configure the HTTP request pipeline
            // applicatiobBuilder
            //      .UseApplicationServices()
            //      .UseInfrastructureServices()
            //      .UseApiServices();

            return applicationBuilder;
        }
    }
}
