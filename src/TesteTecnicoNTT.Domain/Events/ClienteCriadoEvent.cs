using TesteTecnicoNTT.Domain.Common;

namespace TesteTecnicoNTT.Domain.Events
{
    public class ClienteCriadoEvent : DomainEvent
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string NumeroContrato { get; }
        public decimal RendaBruta { get; }
        public string Estado { get; }
        public string Cidade { get; }
        public string CpfCnpj { get; }

        public ClienteCriadoEvent(){}

        public ClienteCriadoEvent(Guid clienteId, string nome, string numeroContrato, decimal rendaBruta, string estado, string cidade, string cpfCnpj)
        {
            Id = clienteId;
            Nome = nome;
            NumeroContrato = numeroContrato;
            RendaBruta = rendaBruta;
            Estado = estado;
            Cidade = cidade;
            CpfCnpj = cpfCnpj;
        }
    }
}
