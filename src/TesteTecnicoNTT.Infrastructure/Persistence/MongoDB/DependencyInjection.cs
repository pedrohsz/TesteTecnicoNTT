using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure.Persistence.MongoDB.Repositories;

namespace TesteTecnicoNTT.Infrastructure.Persistence.MongoDB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = config.GetSection("MongoDbSettings:ConnectionString").Value;
                options.DatabaseName = config.GetSection("MongoDbSettings:DatabaseName").Value;
            });

            services.AddSingleton<MongoDbContext>();

            // Registra os repositórios de leitura
            services.AddScoped<IClienteReadRepository, ClienteReadRepository>();
            services.AddScoped<IPagamentoReadRepository, PagamentoReadRepository>();

            return services;
        }
    }
}
