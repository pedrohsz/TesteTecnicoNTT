using AutoMapper;
using MediatR;
using Moq;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Create;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Tests.Application.Pagamentos.Handlers
{
    public class AdicionarPagamentoCommandHandlerTests
    {
        private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
        private readonly Mock<IClienteRepository> _clientRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AdicionarPagamentoCommandHandler _handler;
        private readonly Mock<IPublisher> _publisherMock;

        public AdicionarPagamentoCommandHandlerTests()
        {
            _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
            _clientRepositoryMock = new Mock<IClienteRepository>();
            _publisherMock = new Mock<IPublisher>();
            _mapperMock = new Mock<IMapper>();
            _handler = new AdicionarPagamentoCommandHandler(_pagamentoRepositoryMock.Object, _publisherMock.Object, _clientRepositoryMock.Object, _mapperMock.Object);
        }

        // só vai funcionar se o cliente existir
        [Fact]
        public async Task Deve_Criar_Pagamento_Com_Sucesso() 
        {
            // Arrange
            var command = new AdicionarPagamentoCommand
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-3001",
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = StatusPagamento.Pago
            };

            // Act
            var pagamentoId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, pagamentoId);
            _pagamentoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Pagamento>()), Times.Once);
        }
    }
}
