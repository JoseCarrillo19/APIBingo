using APIBingo.Application.Interfaces;
using APIBingo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace APIBingo.Application.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBingoNumberServices, BingoNumberServices>();
            services.AddScoped<IProductServices, ProductServices>();
            return services;
        }
    }
}
