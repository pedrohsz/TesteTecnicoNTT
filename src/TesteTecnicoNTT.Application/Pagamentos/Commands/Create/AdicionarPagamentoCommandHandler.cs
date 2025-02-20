using AutoMapper;
using MediatR;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Events;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Create
{
    public class AdicionarPagamentoCommandHandler : IRequestHandler<AdicionarPagamentoCommand, Guid>
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPublisher _publisher;
        private readonly IMapper _mapper;

        public AdicionarPagamentoCommandHandler(IPagamentoRepository pagamentoRepository, IPublisher publisher, IClienteRepository clienteRepository, IMapper mapper)
        {
            _pagamentoRepository = pagamentoRepository;
            _publisher = publisher;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AdicionarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");

            var pagamentos = await _pagamentoRepository.GetByClienteIdAsync(request.ClienteId);
            decimal totalPagamentosPendentes = pagamentos
                .Where(p => p.Status == StatusPagamento.Atrasado || p.Status == StatusPagamento.AVencer)
                .Sum(p => p.Valor);

            if (totalPagamentosPendentes + request.Valor > cliente.RendaBruta)
                throw new InvalidOperationException("O valor total dos pagamentos não pode exceder a renda bruta do cliente.");

            var pagamento = _mapper.Map<Pagamento>(request);

            await _pagamentoRepository.AddAsync(pagamento);

            //var pagamentoEvent = _mapper.Map<PagamentoCriadoEvent>(request);
            var pagamentoEvent = new PagamentoCriadoEvent(pagamento.Id, pagamento.ClienteId, pagamento.Valor, pagamento.NumeroContrato, pagamento.Parcela, pagamento.DataPagamento, (int)pagamento.Status);

            await _publisher.Publish(pagamentoEvent, cancellationToken);

            return pagamento.Id;
        }
    }
}
