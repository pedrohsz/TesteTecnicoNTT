using Moq;
using TesteTecnicoNTT.Application.Clientes.Commands.Delete;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Tests.Application.Clientes.Commands
{
    public class ExcluirClienteCommandHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ExcluirClienteCommandHandler _handler;

        public ExcluirClienteCommandHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _handler = new ExcluirClienteCommandHandler(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Excluir_Cliente_Com_Sucesso()
        {
            // Arrange
            var clienteExistente = new Cliente("12345678901", "Cliente Teste", "CTR-001", "Curitiba", "PR", 5000.00m);
            _clienteRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(clienteExistente);

            var command = new ExcluirClienteCommand { Id = clienteExistente.Id };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _clienteRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Cliente_Nao_Existir()
        {
            // Arrange
            _clienteRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Cliente)null);

            var command = new ExcluirClienteCommand { Id = Guid.NewGuid() };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            _clienteRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
