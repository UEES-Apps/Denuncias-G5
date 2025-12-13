using G5.Denuncias.BE.Domain.Denuncias;
using G5.Denuncias.BE.Infraestructure.Context;
using G5.Denuncias.BE.Infraestructure.Repository.Denuncias;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace G5.Denuncias.BE.Infraestructure.Factory
{
    public static class DenunciaRepositoryFactory
    {
        public static IDenunciaRepository Create(IConfiguration config, IServiceProvider sp)
        {
            var useInMemory = config.GetValue<bool>("UseInMemorySession");
            if (useInMemory)
            {
                var httpAccessor = (IHttpContextAccessor)sp.GetService(typeof(IHttpContextAccessor))!;
                return new DenunciaRepository(httpAccessor, config);
            }
            else
            {
                var db = (DenunciasDbContext)sp.GetService(typeof(DenunciasDbContext))!;
                return new EfDenunciaRepository(db, config);
            }
        }
    }
}
