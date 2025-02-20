using TesteTecnicoNTT.Domain.Common;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.Domain.Entities
{
    public class Pagamento : BaseEntity
    {
        public Guid ClienteId { get; private set; }
        public string NumeroContrato { get; private set; }
        public int Parcela { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public StatusPagamento Status { get; private set; }

        // EF Core
        public Cliente Cliente { get; set; } // testar set privado

        public Pagamento() { }

        public Pagamento(Guid clienteId, string numeroContrato, int parcela, decimal valor, DateTime dataPagamento, StatusPagamento status, Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            ClienteId = clienteId;
            NumeroContrato = numeroContrato;
            Parcela = parcela;
            Valor = valor;
            DataPagamento = dataPagamento;
            Status = status;
        }

        public void AtualizarPagamento(decimal novoValor, DateTime novaData, StatusPagamento novoStatus)
        {
            Valor = novoValor;
            DataPagamento = novaData;
            Status = novoStatus;
        }
    }
}
