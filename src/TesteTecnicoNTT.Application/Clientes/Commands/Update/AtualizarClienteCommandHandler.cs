using MediatR;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Update
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null) return false;

            cliente.AtualizarNome(request.Nome);
            cliente.AtualizarCidade(request.Cidade);
            cliente.AtualizarEstado(request.Estado);
            cliente.AtualizarRendaBruta(request.RendaBruta);

            await _clienteRepository.UpdateAsync(cliente);
            return true;
        }
    }
}
