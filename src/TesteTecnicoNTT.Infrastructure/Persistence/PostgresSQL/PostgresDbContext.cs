using Microsoft.EntityFrameworkCore;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Configurations;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PagamentoConfiguration());
        }
    }
}
