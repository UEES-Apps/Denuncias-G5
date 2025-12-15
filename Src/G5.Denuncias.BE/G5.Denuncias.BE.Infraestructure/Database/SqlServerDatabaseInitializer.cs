using G5.Denuncias.BE.Domain.Database;
using G5.Denuncias.BE.Infraestructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Infraestructure.Database
{
    public class SqlServerDatabaseInitializer : IDatabaseInitializer
    {
        private readonly DenunciasDbContext _db;

        public SqlServerDatabaseInitializer(DenunciasDbContext db)
        {
            _db = db;
        }

        public async Task InitializeAsync()
        {
            await _db.Database.MigrateAsync();
        }
    }
}
