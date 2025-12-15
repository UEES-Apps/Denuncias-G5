using G5.Denuncias.BE.Domain.Database;
using G5.Denuncias.BE.Domain.Denuncias;
using G5.Denuncias.BE.Infraestructure.Context;
using G5.Denuncias.BE.Infraestructure.Repository.Denuncias;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace G5.Denuncias.BE.Infraestructure.Factory
{
    public static class DenunciaRepositoryFactory
    {
        public static async Task<IDenunciaRepository> CreateAsync(IConfiguration config, IServiceProvider sp)
        {
            var useInMemory = config.GetValue<bool>("UseInMemorySession");

            if (useInMemory)
            {
                var httpAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return new DenunciaRepository(httpAccessor, config);
            }

            var db = sp.GetRequiredService<DenunciasDbContext>();

            return new EfDenunciaRepository(db, config);
        }
    }
}
