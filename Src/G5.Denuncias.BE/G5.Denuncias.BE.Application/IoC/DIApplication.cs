using System.Diagnostics.CodeAnalysis;
using G5.Denuncias.BE.Application.Denuncias;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace G5.Denuncias.BE.Application.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DIApplication
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Denuncias
            services.AddScoped<IDenunciasApp, DenunciasApp>();

            return services;
        }
    }
}
