using MediatR;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Clientes.Queries
{
    public class ObterClientePorIdQueryHandler : IRequestHandler<ObterClientePorIdQuery, Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorIdQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _clienteRepository.GetByIdAsync(request.Id);
        }
    }
}
