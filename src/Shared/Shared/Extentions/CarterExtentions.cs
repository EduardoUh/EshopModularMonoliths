namespace Shared.Extentions
{
    public static class CarterExtentions
    {
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies)
                {
                    var moduleCarterImplementations = assembly.GetTypes()
                                  .Where(type => type.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(moduleCarterImplementations);
                }
            });

            return services;
        }
    }
}
