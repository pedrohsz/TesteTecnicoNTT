using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoNTT.Application.Clientes.Commands.Update;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.UnitTests.Application.Clientes.Commands
{
    public class AtualizarClienteCommandHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly AtualizarClienteCommandHandler _handler;

        public AtualizarClienteCommandHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _handler = new AtualizarClienteCommandHandler(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Atualizar_Cliente_Corretamente()
        {
            // Arrange
            var clienteExistente = new Cliente("12345678901", "João Teste", "CTR-0001", "São Paulo", "SP", 5000.00m);
            _clienteRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(clienteExistente);

            var command = new AtualizarClienteCommand
            {
                Id = clienteExistente.Id,
                Nome = "João Atualizado",
                Cidade = "Rio de Janeiro",
                Estado = "RJ",
                RendaBruta = 7000.00m
            };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            Assert.Equal("João Atualizado", clienteExistente.Nome);
            _clienteRepositoryMock.Verify(r => r.UpdateAsync(clienteExistente), Times.Once);
        }
    }
}
