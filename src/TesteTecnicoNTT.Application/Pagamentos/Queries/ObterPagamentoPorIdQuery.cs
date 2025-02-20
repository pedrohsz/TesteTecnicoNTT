using MediatR;
using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Application.Pagamentos.Queries
{
    public class ObterPagamentoPorIdQuery : IRequest<Pagamento>
    {
        public Guid Id { get; set; }
              
    }
}
