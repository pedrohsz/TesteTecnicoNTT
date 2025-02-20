using MediatR;
using System;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Create
{
    public class AdicionarPagamentoCommand : IRequest<Guid>
    {
        public Guid ClienteId { get; set; }
        public string NumeroContrato { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public StatusPagamento Status { get; set; }
    }
}
