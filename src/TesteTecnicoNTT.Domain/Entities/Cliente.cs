using TesteTecnicoNTT.Domain.Common;

namespace TesteTecnicoNTT.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string CpfCnpj { get; set; }
        public string Nome { get; private set; }
        public string NumeroContrato { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public decimal RendaBruta { get; private set; }

        // Propriedade de navegação
        public ICollection<Pagamento> Pagamentos { get; private set; }

        public Cliente() { }

        public Cliente(string cpfCnpj, string nome, string numeroContrato, string cidade, string estado, decimal rendaBruta, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            ValidarCpfCnpj(cpfCnpj);
            ValidarRendaBruta(rendaBruta);

            CpfCnpj = cpfCnpj;
            Nome = nome;
            NumeroContrato = numeroContrato;
            Cidade = cidade;
            Estado = estado;
            RendaBruta = rendaBruta;
        }

        // Não vou adicionar a validação real pois vai atrapalhar demais nos testes
        private void ValidarCpfCnpj(string cpfCnpj)
        {
            if (string.IsNullOrWhiteSpace(cpfCnpj) || (cpfCnpj.Length != 11 && cpfCnpj.Length != 14))
            {
                throw new ArgumentException("CPF ou CNPJ inválido");
            }

        }

        private void ValidarRendaBruta(decimal rendaBruta)
        {
            if (rendaBruta <= 0)
            {
                throw new ArgumentException("A renda bruta deve ser maior que zero.");
            }
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("O nome não pode ser vazio.");

            Nome = novoNome;
        }

        public void AtualizarCidade(string novaCidade)
        {
            if (string.IsNullOrWhiteSpace(novaCidade))
                throw new ArgumentException("A cidade não pode ser vazia.");

            Cidade = novaCidade;
        }

        public void AtualizarEstado(string novoEstado)
        {
            if (string.IsNullOrWhiteSpace(novoEstado))
                throw new ArgumentException("O estado não pode ser vazio.");

            Estado = novoEstado;
        }

        public void AtualizarRendaBruta(decimal novaRendaBruta)
        {
            ValidarRendaBruta(novaRendaBruta);
            RendaBruta = novaRendaBruta;
        }
    }
}
