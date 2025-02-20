using MediatR;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Delete
{
    public class ExcluirPagamentoCommandHandler : IRequestHandler<ExcluirPagamentoCommand, bool>
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public ExcluirPagamentoCommandHandler(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<bool> Handle(ExcluirPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(request.Id);
            if (pagamento == null) return false;

            await _pagamentoRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
