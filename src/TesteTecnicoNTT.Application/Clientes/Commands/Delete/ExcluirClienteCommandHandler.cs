using MediatR;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Delete
{
    public class ExcluirClienteCommandHandler : IRequestHandler<ExcluirClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public ExcluirClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(ExcluirClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null) return false;

            await _clienteRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
