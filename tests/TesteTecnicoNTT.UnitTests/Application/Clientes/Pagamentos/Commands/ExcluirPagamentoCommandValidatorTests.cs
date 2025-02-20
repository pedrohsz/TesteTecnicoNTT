using System;
using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Delete;
using Xunit;

namespace TesteTecnicoNTT.Tests.Application.Pagamentos.Commands
{
    public class ExcluirPagamentoCommandValidatorTests
    {
        private readonly ExcluirPagamentoCommandValidator _validator;

        public ExcluirPagamentoCommandValidatorTests()
        {
            _validator = new ExcluirPagamentoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Id_For_Vazio()
        {
            var command = new ExcluirPagamentoCommand { Id = Guid.Empty };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Id);
        }

        [Fact]
        public void Deve_Passar_Se_Id_For_Valido()
        {
            var command = new ExcluirPagamentoCommand { Id = Guid.NewGuid() };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
