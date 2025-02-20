using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TesteTecnicoNTT.Infrastructure.Kafka.Models
{
    public class ClienteDto
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NumeroContrato { get; set; }
        public decimal RendaBruta { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string CpfCnpj { get; set; }
    }
}