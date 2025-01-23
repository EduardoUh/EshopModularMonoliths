namespace Shared.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder applicationBuilder) where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();

            SeedDataAsync(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();

            return applicationBuilder;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            await context.Database.MigrateAsync();
        }

        private static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();

            foreach (var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }

        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(reference =>
                reference.TargetEntry != null &&
                reference.TargetEntry.Metadata.IsOwned() &&
                (reference.TargetEntry.State == EntityState.Added || reference.TargetEntry.State == EntityState.Modified));
    }
}
