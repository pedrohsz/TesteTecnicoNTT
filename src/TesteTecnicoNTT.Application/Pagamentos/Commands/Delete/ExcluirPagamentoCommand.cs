using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Delete
{
    public class ExcluirPagamentoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
