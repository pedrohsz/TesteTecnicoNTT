using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Create;
using TesteTecnicoNTT.Domain.Enums;

namespace TesteTecnicoNTT.UnitTests.Application.Clientes.Commands.Pagamentos
{
    public class AdicionarPagamentoCommandValidatorTests
    {
        private readonly AdicionarPagamentoCommandValidator _validator;

        public AdicionarPagamentoCommandValidatorTests()
        {
            _validator = new AdicionarPagamentoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Campos_Obrigatorios_Nao_Forem_Preenchidos()
        {
            var command = new AdicionarPagamentoCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.ClienteId);
            result.ShouldHaveValidationErrorFor(p => p.NumeroContrato);
            result.ShouldHaveValidationErrorFor(p => p.Parcela);
            result.ShouldHaveValidationErrorFor(p => p.Valor);
        }

        [Fact]
        public void Deve_Passar_Se_Todos_Os_Campos_Estiverem_Certos()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-1001",
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Deve_Falhar_Se_ClienteId_For_Invalido()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.Empty,
                NumeroContrato = "CTR-1001",
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.ClienteId);
        }

        [Fact]
        public void Deve_Falhar_Se_NumeroContrato_For_Invalido()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = string.Empty,
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.NumeroContrato);
        }

        [Fact]
        public void Deve_Falhar_Se_Parcela_For_Negativa()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-1001",
                Parcela = -1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Parcela);
        }

        [Fact]
        public void Deve_Falhar_Se_Valor_For_Negativo()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-1001",
                Parcela = 1,
                Valor = -1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Valor);
        }

        [Fact]
        public void Deve_Falhar_Se_DataPagamento_For_Invalida()
        {
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-1001",
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.MinValue,
                Status = StatusPagamento.Pago
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.DataPagamento);
        }
    }
}
