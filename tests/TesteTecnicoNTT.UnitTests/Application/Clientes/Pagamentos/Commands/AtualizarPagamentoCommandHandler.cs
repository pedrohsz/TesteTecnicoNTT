using Moq;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Update;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Tests.Application.Pagamentos.Handlers
{
    public class AtualizarPagamentoCommandHandlerTests
    {
        private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
        private readonly AtualizarPagamentoCommandHandler _handler;

        public AtualizarPagamentoCommandHandlerTests()
        {
            _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
            _handler = new AtualizarPagamentoCommandHandler(_pagamentoRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Atualizar_Pagamento_Com_Sucesso()
        {
            // Arrange
            var pagamentoExistente = new Pagamento(Guid.NewGuid(), "CTR-5001", 1, 1500.00m, DateTime.UtcNow.AddDays(5), StatusPagamento.AVencer);
            _pagamentoRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pagamentoExistente);

            var command = new AtualizarPagamentoCommand
            {
                Id = pagamentoExistente.Id,
                NovoValor = 2000.00m,
                NovaData = DateTime.UtcNow.AddDays(3),
                NovoStatus = StatusPagamento.Pago
            };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _pagamentoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Pagamento>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Falhar_Se_Pagamento_Nao_Existir()
        {
            // Arrange
            _pagamentoRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pagamento)null);

            var command = new AtualizarPagamentoCommand
            {
                Id = Guid.NewGuid(),
                NovoValor = 2000.00m,
                NovaData = DateTime.UtcNow.AddDays(3),
                NovoStatus = StatusPagamento.Pago
            };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            _pagamentoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Pagamento>()), Times.Never);
        }
    }
}
