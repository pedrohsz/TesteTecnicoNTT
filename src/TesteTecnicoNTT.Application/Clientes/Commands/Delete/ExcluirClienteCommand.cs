using MediatR;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Delete
{
    public class ExcluirClienteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
