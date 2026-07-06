using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastucture.Data.DataSeed
{
    public class CatalogDataSeed(StoreDbContext dbContext) : IDataSeeder
    {

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                // Check if there is any Pending Migrations or Not
                var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigration.Any())
                    await dbContext.Database.MigrateAsync(); // Update-Database

                // Path
                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                // The order matters here -> Product Rely on ProductBrand & ProductType
                await SeedDataIfEmptyAsync<ProductBrand, int>(rootPath, "brands.json", ct);
                await SeedDataIfEmptyAsync<ProductType, int>(rootPath, "types.json", ct);
                await SeedDataIfEmptyAsync<Product, int>(rootPath, "products.json", ct);

                var result = await dbContext.SaveChangesAsync(ct);

                if (result > 0)
                {
                    Console.WriteLine($"Seed Data Has Been Seeded Successfully , {result} rows affected");
                }
                else
                {
                    Console.WriteLine($"Seed Data Has Been Failed , No Rows Affected");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // Method To Read Json
        private async Task SeedDataIfEmptyAsync<T , TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                return;
            }
            var filePath = Path.Combine(rootPath , fileName);

            Console.WriteLine("FILE PATH: " + filePath);
            Console.WriteLine("FILE EXISTS: " + File.Exists(filePath));

            if (!File.Exists(filePath)) {
                return;
            }
            // Unmanaged
            using var fileStream = File.OpenRead(filePath);

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);

            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);
        }
    }
}
