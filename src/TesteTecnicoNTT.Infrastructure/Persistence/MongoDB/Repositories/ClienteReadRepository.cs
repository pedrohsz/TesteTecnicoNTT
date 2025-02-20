using MongoDB.Driver;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Persistence.MongoDB.Repositories
{
    public class ClienteReadRepository : IClienteReadRepository
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClienteReadRepository(MongoDbContext mongoDbContext)
        {
            _clientesCollection = mongoDbContext.GetCollection<Cliente>("Clientes");
        }

        public async Task<List<Cliente>> ObterTodosClientesAsync()
        {
            return await _clientesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _clientesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<decimal> ObterMediaRendaBrutaPorEstadoAsync(string estado)
        {
            var aggregate = await _clientesCollection
                .Aggregate()
                .Match(c => c.Estado == estado)
                .Group(c => c.Estado, g => new { Estado = g.Key, MediaRenda = g.Average(x => x.RendaBruta) })
                .FirstOrDefaultAsync();

            return aggregate?.MediaRenda ?? 0;
        }
    }
}
