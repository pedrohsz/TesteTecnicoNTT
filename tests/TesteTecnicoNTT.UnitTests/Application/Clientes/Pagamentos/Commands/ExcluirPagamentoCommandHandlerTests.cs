using Moq;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Delete;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Tests.Application.Pagamentos.Commands
{
    public class ExcluirPagamentoCommandHandlerTests
    {
        private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
        private readonly ExcluirPagamentoCommandHandler _handler;

        public ExcluirPagamentoCommandHandlerTests()
        {
            _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
            _handler = new ExcluirPagamentoCommandHandler(_pagamentoRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Excluir_Pagamento_Com_Sucesso()
        {
            // Arrange
            var pagamentoExistente = new Pagamento(Guid.NewGuid(), "CTR-9999", 1, 5000.00m, DateTime.UtcNow.AddDays(5), StatusPagamento.Pago);
            _pagamentoRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pagamentoExistente);

            var command = new ExcluirPagamentoCommand { Id = pagamentoExistente.Id };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _pagamentoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Pagamento_Nao_Existir()
        {
            // Arrange
            _pagamentoRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pagamento)null);

            var command = new ExcluirPagamentoCommand { Id = Guid.NewGuid() };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            _pagamentoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
