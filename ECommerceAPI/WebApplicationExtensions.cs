using ECommerce.Domain.Contracts;

namespace ECommerceAPI
{
    public static class WebApplicationExtensions
    {
        public static async Task <WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            // Unmamaged
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
         
            await seeder.SeedDataAsync();
            return app;
        }
    }
}
