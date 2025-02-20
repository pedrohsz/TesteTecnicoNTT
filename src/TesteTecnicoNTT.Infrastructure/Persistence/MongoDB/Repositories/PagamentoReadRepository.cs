using MongoDB.Bson;
using MongoDB.Driver;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Persistence.MongoDB.Repositories
{
    public class PagamentoReadRepository : IPagamentoReadRepository
    {
        private readonly IMongoCollection<Pagamento> _pagamentosCollection;

        public PagamentoReadRepository(MongoDbContext mongoDbContext)
        {
            _pagamentosCollection = mongoDbContext.GetCollection<Pagamento>("Pagamentos");
        }

        public async Task<int> ObterTotalPagamentosAsync()
        {
            return (int)await _pagamentosCollection.CountDocumentsAsync(_ => true);
        }

        public async Task<int> ObterTotalPagamentosAtrasadosEVencerAsync()
        {
            var filtro = Builders<Pagamento>.Filter.Or(
                Builders<Pagamento>.Filter.Eq(p => p.Status, StatusPagamento.Atrasado),
                Builders<Pagamento>.Filter.Eq(p => p.Status, StatusPagamento.AVencer)
            );

            return (int)await _pagamentosCollection.CountDocumentsAsync(filtro);
        }

        public async Task<Dictionary<string, int>> ObterPagamentosAgrupadosPorEstadoAsync()
        {
            var aggregate = await _pagamentosCollection
                .Aggregate()
                .Lookup("Clientes", "ClienteId", "_id", "cliente_info")
                .Unwind("cliente_info")
                .Group(new BsonDocument
                {
                    { "_id", "$cliente_info.Estado" },
                    { "Total", new BsonDocument("$sum", 1) }
                })
                .ToListAsync();

            return aggregate.ToDictionary(
                p => p["_id"].AsString,
                p => p["Total"].AsInt32
            );
        }

        public async Task<Dictionary<string, double>> ObterMediaRendaPorStatusAsync()
        {
            var aggregate = await _pagamentosCollection
                .Aggregate()
                .Lookup("Clientes", "ClienteId", "_id", "cliente_info")
                .Unwind("cliente_info")
                .Group(new BsonDocument
                {
                    { "_id", "$Status" },
                    { "MediaRenda", new BsonDocument("$avg", "$cliente_info.RendaBruta") }
                })
                .ToListAsync();

            return aggregate.ToDictionary(
                p => ((StatusPagamento)p["_id"].AsInt32).ToString(),
                p => p["MediaRenda"].IsDouble ? p["MediaRenda"].AsDouble : (double)p["MediaRenda"].AsDecimal128
            );
        }

        public async Task<Dictionary<string, Dictionary<string, int>>> ObterClientesPorEstadoEStatusAsync()
        {
            var aggregate = await _pagamentosCollection
                .Aggregate()
                .Lookup("Clientes", "ClienteId", "_id", "cliente_info")
                .Unwind("cliente_info")
                .Group(new BsonDocument
                {
                    { "_id", new BsonDocument { { "Estado", "$cliente_info.Estado" }, { "Status", "$Status" } } },
                    { "TotalClientes", new BsonDocument("$sum", 1) }
                })
                .ToListAsync();

            var resultado = new Dictionary<string, Dictionary<string, int>>();

            foreach (var item in aggregate)
            {
                var estado = item["_id"]["Estado"].AsString;
                var status = ((StatusPagamento)item["_id"]["Status"].AsInt32).ToString();
                var total = item["TotalClientes"].AsInt32;

                if (!resultado.ContainsKey(estado))
                {
                    resultado[estado] = new Dictionary<string, int>();
                }

                resultado[estado][status] = total;
            }

            return resultado;
        }
    }
}