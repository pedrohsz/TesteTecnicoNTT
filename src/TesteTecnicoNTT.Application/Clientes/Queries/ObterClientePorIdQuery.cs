using MediatR;
using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Application.Clientes.Queries
{
    public class ObterClientePorIdQuery : IRequest<Cliente>
    {
        public Guid Id { get; set; }
    }
}
