using G5.Denuncias.BE.Domain.Denuncias.Entities;
using Microsoft.EntityFrameworkCore;

namespace G5.Denuncias.BE.Infraestructure.Context
{
    public class DenunciasDbContext : DbContext
    {
        public DenunciasDbContext(DbContextOptions<DenunciasDbContext> opts) : base(opts) { }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Denuncia> Denuncias { get; set; } = null!;
        public DbSet<Mensaje> Mensajes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Denuncia>().HasKey(d => d.Id);
            modelBuilder.Entity<Mensaje>().HasKey(m => m.Id);
        }
    }
}
