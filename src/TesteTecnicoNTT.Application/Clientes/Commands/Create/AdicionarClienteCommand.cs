using MediatR;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Create
{
    public class AdicionarClienteCommand : IRequest<Guid>
    {
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string NumeroContrato { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal RendaBruta { get; set; }
    }
}
