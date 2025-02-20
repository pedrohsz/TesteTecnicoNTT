using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Clientes.Commands.Create;

namespace TesteTecnicoNTT.UnitTests.Application.Clientes.Commands
{
    public class AdicionarClienteCommandValidatorTests
    {
        private readonly AdicionarClienteCommandValidator _validator;

        public AdicionarClienteCommandValidatorTests()
        {
            _validator = new AdicionarClienteCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Campos_Obrigatorios_Nao_Forem_Preenchidos()
        {
            var command = new AdicionarClienteCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.CpfCnpj);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
            result.ShouldHaveValidationErrorFor(c => c.NumeroContrato);
            result.ShouldHaveValidationErrorFor(c => c.Cidade);
            result.ShouldHaveValidationErrorFor(c => c.Estado);
            result.ShouldHaveValidationErrorFor(c => c.RendaBruta);
        }

        [Fact]
        public void Deve_Passar_Se_Todos_Os_Campos_Estiverem_Certos()
        {
            var command = new AdicionarClienteCommand
            {
                CpfCnpj = "12345678901",
                Nome = "Cliente Teste",
                NumeroContrato = "CTR-1234",
                Cidade = "São Paulo",
                Estado = "SP",
                RendaBruta = 5000.00m
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
