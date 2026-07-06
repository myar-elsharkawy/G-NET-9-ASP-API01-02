using ECommerce.Domain.Contracts;
using ECommerce.Infrastucture.Data;
using ECommerce.Infrastucture.Data.DataSeed;
using ECommerce.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastucture
{
    
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services ,IConfiguration configuration) {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeed>("Catalog"); // Issue solved with key
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        
        }
    }
}
