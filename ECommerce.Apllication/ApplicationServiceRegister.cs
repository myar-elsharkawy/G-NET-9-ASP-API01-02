using ECommerce.Application.Contracts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            // services.AddAutoMapper(c => c.AddProfile(new[] { new ProductProfiles() }));

            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegister).Assembly);

            services.AddScoped<IProductService, ProductService>();
            return services;
        }

    }
}
