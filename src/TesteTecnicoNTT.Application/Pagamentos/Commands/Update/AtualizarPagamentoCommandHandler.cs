using MediatR;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Update
{
    public class AtualizarPagamentoCommandHandler : IRequestHandler<AtualizarPagamentoCommand, bool>
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public AtualizarPagamentoCommandHandler(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<bool> Handle(AtualizarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(request.Id);
            if (pagamento == null) return false;

            pagamento.AtualizarPagamento(request.NovoValor, request.NovaData, request.NovoStatus);
            await _pagamentoRepository.UpdateAsync(pagamento);

            return true;
        }
    }
}
