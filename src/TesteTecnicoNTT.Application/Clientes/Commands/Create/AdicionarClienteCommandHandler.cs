using MediatR;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Events;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Create
{
    public class AdicionarClienteCommandHandler : IRequestHandler<AdicionarClienteCommand, Guid>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediator _mediator;

        public AdicionarClienteCommandHandler(IClienteRepository clienteRepository, IMediator mediator)
        {
            _clienteRepository = clienteRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteExistente = await _clienteRepository.ObterPorCpfCnpjAsync(request.CpfCnpj);
            if (clienteExistente != null)
                throw new InvalidOperationException("Já existe um cliente cadastrado com esse CPF/CNPJ.");

            var cliente = new Cliente(request.CpfCnpj, request.Nome, request.NumeroContrato, request.Cidade, request.Estado, request.RendaBruta);
            await _clienteRepository.AddAsync(cliente);

            // Dispara o evento após a criação do cliente
            await _mediator.Publish(new ClienteCriadoEvent(cliente.Id, cliente.Nome, cliente.NumeroContrato, cliente.RendaBruta, cliente.Estado, cliente.Cidade, cliente.CpfCnpj));

            return cliente.Id;
        }
    }
}
