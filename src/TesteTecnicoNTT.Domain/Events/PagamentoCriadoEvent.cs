using TesteTecnicoNTT.Domain.Common;

namespace TesteTecnicoNTT.Domain.Events
{
    public class PagamentoCriadoEvent : DomainEvent
    {
        public Guid Id { get; }
        public Guid ClienteId { get; }
        public decimal Valor { get; }
        public string NumeroContrato { get; }
        public int Parcela { get; }
        public DateTime DataPagamento { get; }
        public int Status { get; }

        public PagamentoCriadoEvent() { }

        public PagamentoCriadoEvent(Guid pagamentoId, Guid clienteId, decimal valor, string numeroContrato, int parcela, DateTime dataPagamento, int status)
        {
            Id = pagamentoId;
            ClienteId = clienteId;
            Valor = valor;
            NumeroContrato = numeroContrato;
            Parcela = parcela;
            DataPagamento = dataPagamento;
            Status = status;
        }
    }
}
