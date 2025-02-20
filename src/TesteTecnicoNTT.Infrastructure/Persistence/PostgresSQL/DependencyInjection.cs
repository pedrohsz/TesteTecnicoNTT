using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Repositories;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgreSqlPersistence(this IServiceCollection services, IConfiguration config)
        {
            // Configurar PostgreSQL
            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("PostgresConnection")));

            // Registra os repositórios de escrita (CQRS - Command)
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            return services;
        }
    }
}
