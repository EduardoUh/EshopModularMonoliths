﻿namespace Shared.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder applicationBuilder) where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();

            return applicationBuilder;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            await context.Database.MigrateAsync();
        }
    }
}