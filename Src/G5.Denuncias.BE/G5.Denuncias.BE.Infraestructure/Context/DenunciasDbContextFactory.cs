using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Infraestructure.Context
{
    public class DenunciasDbContextFactory
        : IDesignTimeDbContextFactory<DenunciasDbContext>
    {
        public DenunciasDbContext CreateDbContext(string[] args)
        {
            // Leer appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DenunciasDbContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("Denuncias"),
                sql => sql.MigrationsAssembly(typeof(DenunciasDbContext).Assembly.FullName)
            );

            return new DenunciasDbContext(optionsBuilder.Options);
        }
    }
}
