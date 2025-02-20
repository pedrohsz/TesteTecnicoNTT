using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.Infrastructure.Kafka.Models
{
    public class PagamentoDto
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ClienteId { get; set; }
        public string NumeroContrato { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public StatusPagamento Status { get; set; }

    }
}
