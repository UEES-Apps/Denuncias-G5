using G5.Denuncias.BE.Domain.Database;
using G5.Denuncias.BE.Domain.Denuncias;
using G5.Denuncias.BE.Infraestructure.Context;
using G5.Denuncias.BE.Infraestructure.Database;
using G5.Denuncias.BE.Infraestructure.Factory;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace G5.Denuncias.BE.Infraestructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DllInfraestructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Session (in-memory session)
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddHttpContextAccessor();

            // EF Core (SQL Server)
            services.AddDbContext<DenunciasDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Denuncias"));
            });

            // Denuncias Repository
            services.AddScoped<IDatabaseInitializer, SqlServerDatabaseInitializer>();

            services.AddScoped<IDenunciaRepository>(serviceProvider =>
            {
                return DenunciaRepositoryFactory.CreateAsync(configuration, serviceProvider).GetAwaiter().GetResult();
            });

            return services;
        }
    }
}
