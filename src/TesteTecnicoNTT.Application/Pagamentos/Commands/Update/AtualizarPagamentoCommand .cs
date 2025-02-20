using MediatR;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Update
{
    public class AtualizarPagamentoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public decimal NovoValor { get; set; }
        public DateTime NovaData { get; set; }
        public StatusPagamento NovoStatus { get; set; }
    }
}
