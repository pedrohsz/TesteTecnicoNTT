using MediatR;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Pagamentos.Queries
{
    public class ObterTotalPagamentosQuery : IRequest<long> { }
    public class ObterTotalPagamentosAtrasadosOuAVencerQuery : IRequest<long> { }
    public class ObterPagamentosPorEstadoQuery : IRequest<Dictionary<string, int>> { }
    public class ObterMediaRendaPorEstadoQuery : IRequest<Dictionary<string, double>> { }
    public class ObterClientesPorEstadoEStatusQuery : IRequest<Dictionary<string, Dictionary<string, int>>> { }


    public class PagamentoQueryHandler :
        IRequestHandler<ObterTotalPagamentosQuery, long>,
        IRequestHandler<ObterTotalPagamentosAtrasadosOuAVencerQuery, long>,
        IRequestHandler<ObterPagamentosPorEstadoQuery, Dictionary<string, int>>,
        IRequestHandler<ObterMediaRendaPorEstadoQuery, Dictionary<string, double>>,
        IRequestHandler<ObterClientesPorEstadoEStatusQuery, Dictionary<string, Dictionary<string, int>>>
    {
        private readonly IPagamentoReadRepository _pagamentoReadRepository;

        public PagamentoQueryHandler(IPagamentoReadRepository pagamentoReadRepository)
        {
            _pagamentoReadRepository = pagamentoReadRepository;
        }

        public async Task<long> Handle(ObterTotalPagamentosQuery request, CancellationToken cancellationToken)
        {
            return await _pagamentoReadRepository.ObterTotalPagamentosAsync();
        }

        public async Task<long> Handle(ObterTotalPagamentosAtrasadosOuAVencerQuery request, CancellationToken cancellationToken)
        {
            return await _pagamentoReadRepository.ObterTotalPagamentosAtrasadosEVencerAsync();
        }

        public async Task<Dictionary<string, int>> Handle(ObterPagamentosPorEstadoQuery request, CancellationToken cancellationToken)
        {
            return await _pagamentoReadRepository.ObterPagamentosAgrupadosPorEstadoAsync();
        }

        public async Task<Dictionary<string, double>> Handle(ObterMediaRendaPorEstadoQuery request, CancellationToken cancellationToken)
        {
            return await _pagamentoReadRepository.ObterMediaRendaPorStatusAsync();
        }

        public async Task<Dictionary<string, Dictionary<string, int>>> Handle(ObterClientesPorEstadoEStatusQuery request, CancellationToken cancellationToken)
        {
            return await _pagamentoReadRepository.ObterClientesPorEstadoEStatusAsync();
        }
    }
}
