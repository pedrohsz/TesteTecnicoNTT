using MediatR;
using Moq;
using TesteTecnicoNTT.Application.Clientes.Commands.Create;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.UnitTests.Application.Clientes.Commands
{
    public class AdicionarClienteCommandHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AdicionarClienteCommandHandler _handler;

        public AdicionarClienteCommandHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new AdicionarClienteCommandHandler(_clienteRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Cliente_Corretamente()
        {
            // Arrange
            var command = new AdicionarClienteCommand
            {
                CpfCnpj = "12345678901",
                Nome = "João Teste",
                NumeroContrato = "CTR-0001",
                Cidade = "São Paulo",
                Estado = "SP",
                RendaBruta = 5000.00m
            };

            // Act
            var clienteId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, clienteId);
            _clienteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
