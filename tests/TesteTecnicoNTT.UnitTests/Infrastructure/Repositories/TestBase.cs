using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL;

namespace TesteTecnicoNTT.UnitTests.Infrastructure.Repositories
{
    public abstract class TestBase : IDisposable
    {
        protected readonly PostgresDbContext _context;
        private readonly string _connectionString;

        public TestBase()
        {
            // Carrega a ConnectionString do appsettings.Test.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("PostgresConnection");

            var options = new DbContextOptionsBuilder<PostgresDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            _context = new PostgresDbContext(options);

            _context.Database.EnsureCreated();

            // 🔹 Evita rodar `Migrate()` se as tabelas já estiverem no banco
            if (!_context.Database.GetMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }

        public void Dispose()
        {
            // 🔹 Limpa os dados sem excluir as tabelas
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Clientes\" RESTART IDENTITY CASCADE;");
            _context.SaveChanges();

            _context.Dispose();
        }
    }
}
