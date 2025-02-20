using System;
using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Clientes.Commands.Delete;
using Xunit;

namespace TesteTecnicoNTT.Tests.Application.Clientes.Commands
{
    public class ExcluirClienteCommandValidatorTests
    {
        private readonly ExcluirClienteCommandValidator _validator;

        public ExcluirClienteCommandValidatorTests()
        {
            _validator = new ExcluirClienteCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Id_For_Vazio()
        {
            var command = new ExcluirClienteCommand { Id = Guid.Empty };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void Deve_Passar_Se_Id_For_Valido()
        {
            var command = new ExcluirClienteCommand { Id = Guid.NewGuid() };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
