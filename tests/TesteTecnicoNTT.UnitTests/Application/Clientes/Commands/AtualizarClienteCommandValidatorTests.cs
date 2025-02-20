using System;
using FluentValidation.TestHelper;
using TesteTecnicoNTT.Application.Clientes.Commands.Update;
using Xunit;

namespace TesteTecnicoNTT.Tests.Application.Clientes.Commands
{
    public class AtualizarClienteCommandValidatorTests
    {
        private readonly AtualizarClienteCommandValidator _validator;

        public AtualizarClienteCommandValidatorTests()
        {
            _validator = new AtualizarClienteCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Se_Campos_Obrigatorios_Nao_Forem_Preenchidos()
        {
            var command = new AtualizarClienteCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Id);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
            //result.ShouldHaveValidationErrorFor(c => c.NumeroContrato);
            result.ShouldHaveValidationErrorFor(c => c.Cidade);
            result.ShouldHaveValidationErrorFor(c => c.Estado);
            result.ShouldHaveValidationErrorFor(c => c.RendaBruta);
        }

        [Fact]
        public void Deve_Passar_Se_Todos_Os_Campos_Estiverem_Certos()
        {
            var command = new AtualizarClienteCommand
            {
                Id = Guid.NewGuid(),
                Nome = "Cliente Atualizado",
                //NumeroContrato = "CTR-2024-001",
                Cidade = "Rio de Janeiro",
                Estado = "RJ",
                RendaBruta = 7500.00m
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
