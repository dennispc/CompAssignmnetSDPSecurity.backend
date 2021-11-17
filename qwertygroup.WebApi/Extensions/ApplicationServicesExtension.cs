using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.DataAccess.Repositories;
using CompAssignmnetSDPSecurity.Domain;
using CompAssignmnetSDPSecurity.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompAssignmnetSDPSecurity.WebApi.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
        
       }
}