using APIBingo.Domain.IRepositories;
using APIBingo.Infrastructure.Data;
using APIBingo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RaffleAPI.Infrastructure.Security.Security;

namespace APIBingo.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            string serverName = "(localdb)\\mssqllocaldb";
            string dataBaseName = "Bingo";

            string connectionString = $"Server={serverName};Database={dataBaseName};Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

            services.AddTransient<SqlConnection>(_ => new SqlConnection(connectionString));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBingoNumberRepository, BingoNumberRepository>();

            services.AddAuthentication("ApiKeyAuthentication")
           .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyAuthentication", null);

            return services;
        }
    }
}