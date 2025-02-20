using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Update;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.Tests.Application.Pagamentos.Commands
{
    public class AtualizarPagamentoCommandValidatorTests
    {
        private readonly AtualizarPagamentoCommandValidator _validator;

        public AtualizarPagamentoCommandValidatorTests()
        {
            _validator = new AtualizarPagamentoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Campos_Obrigatorios_Nao_Forem_Preenchidos()
        {
            var command = new AtualizarPagamentoCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Id);
            result.ShouldHaveValidationErrorFor(p => p.NovoValor);
            result.ShouldHaveValidationErrorFor(p => p.NovaData);
        }

        [Fact]
        public void Deve_Passar_Se_Todos_Os_Campos_Estiverem_Certos()
        {
            var command = new AtualizarPagamentoCommand
            {
                Id = Guid.NewGuid(),
                NovoValor = 2000.00m,
                NovaData = DateTime.UtcNow.AddDays(3),
                NovoStatus = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
