using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure.Kafka;
using TesteTecnicoNTT.Infrastructure.Kafka.Models;
using TesteTecnicoNTT.Infrastructure.Persistence.MongoDB;

public class KafkaTests
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IKafkaConsumer _kafkaConsumer;
    private readonly MongoDbContext _mongoDbContext;

    public KafkaTests()
    {
        var settingsMock = new Mock<IOptions<MongoDbSettings>>();
        settingsMock.Setup(s => s.Value).Returns(new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "Teste"
        });

        _mongoDbContext = new MongoDbContext(settingsMock.Object);
        _kafkaProducer = new KafkaProducer();
        _kafkaConsumer = new KafkaConsumer(_mongoDbContext);
    }

    [Fact]
    public async Task Deve_Enviar_Mensagem_Cliente_No_Kafka()
    {
        // Arrange
        string topic = "cliente-criado";
        var cliente = new ClienteDto
        {
            Id = Guid.NewGuid(),
            Nome = "Teste Kafka Cliente",
            NumeroContrato = "CTR-9999",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            CpfCnpj = "98765432101",
            RendaBruta = 10000
        };

        // Act
        await _kafkaProducer.EnviarMensagemAsync(topic, cliente);

        // Assert - Apenas confirma que a mensagem foi enviada sem erro
        Assert.True(true);
    }

    [Fact]
    public async Task Deve_Enviar_Mensagem_Pagamento_No_Kafka()
    {
        // Arrange
        string topic = "pagamento-criado";
        var pagamento = new PagamentoDto
        {
            Id = Guid.NewGuid(),
            ClienteId = Guid.NewGuid(),
            NumeroContrato = "CTR-8888",
            Parcela = 3,
            Valor = 2500.50m,
            DataPagamento = DateTime.UtcNow,
            Status = TesteTecnicoNTT.Domain.Enums.StatusPagamento.AVencer
        };

        // Act
        await _kafkaProducer.EnviarMensagemAsync(topic, pagamento);

        // Assert - Apenas confirma que a mensagem foi enviada sem erro
        Assert.True(true);
    }

    [Fact]
    public async Task Deve_Enviar_E_Consumir_Mensagem_Cliente_No_Kafka_E_Salvar_MongoDB()
    {
        // Arrange
        string topic = "cliente-criado";
        var clienteEnviado = new ClienteDto
        {
            Id = Guid.NewGuid(),
            Nome = "Teste Kafka Cliente",
            NumeroContrato = "CTR-9999",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            CpfCnpj = "98765432101",
            RendaBruta = 10000
        };

        // Enviar mensagem para o Kafka antes de consumir
        await _kafkaProducer.EnviarMensagemAsync(topic, clienteEnviado);

        // Act - Consumir mensagem do Kafka
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await _kafkaConsumer.ConsumirMensagensAsync(topic, cancellationToken);

        // Assert - Verificar se a mensagem foi salva no MongoDB e comparar os dados
        var collection = _mongoDbContext.GetCollection<ClienteDto>("Clientes");
        var clienteConsumido = await collection.Find(c => c.NumeroContrato == "CTR-9999").FirstOrDefaultAsync();

        Assert.NotNull(clienteConsumido);
        Assert.Equal(clienteEnviado.Nome, clienteConsumido.Nome);
        Assert.Equal(clienteEnviado.CpfCnpj, clienteConsumido.CpfCnpj);
        Assert.Equal(clienteEnviado.NumeroContrato, clienteConsumido.NumeroContrato);
        Assert.Equal(clienteEnviado.Estado, clienteConsumido.Estado);
        Assert.Equal(clienteEnviado.Cidade, clienteConsumido.Cidade);
        Assert.Equal(clienteEnviado.RendaBruta, clienteConsumido.RendaBruta);
    }

    [Fact]
    public async Task Deve_Enviar_E_Consumir_Mensagem_Pagamento_No_Kafka_E_Salvar_MongoDB()
    {
        // Arrange
        string topic = "pagamento-criado";
        var pagamentoEnviado = new PagamentoDto
        {
            Id = Guid.NewGuid(),
            ClienteId = Guid.NewGuid(),
            NumeroContrato = "CTR-8881",
            Parcela = 2,
            Valor = 2500.50m,
            DataPagamento = DateTime.UtcNow,
            Status = TesteTecnicoNTT.Domain.Enums.StatusPagamento.AVencer
        };

        // Enviar mensagem para o Kafka antes de consumir
        await _kafkaProducer.EnviarMensagemAsync(topic, pagamentoEnviado);

        // Act - Consumir mensagem do Kafka
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await _kafkaConsumer.ConsumirMensagensAsync(topic, cancellationToken);

        // Assert - Verificar se a mensagem foi salva no MongoDB e comparar os dados
        var collection = _mongoDbContext.GetCollection<PagamentoDto>("Pagamentos");
        var pagamentoConsumido = await collection.Find(p => p.NumeroContrato == "CTR-8881").FirstOrDefaultAsync();

        Assert.NotNull(pagamentoConsumido);
        Assert.Equal(pagamentoEnviado.ClienteId, pagamentoConsumido.ClienteId);
        Assert.Equal(pagamentoEnviado.NumeroContrato, pagamentoConsumido.NumeroContrato);
        Assert.Equal(pagamentoEnviado.Parcela, pagamentoConsumido.Parcela);
        Assert.Equal(pagamentoEnviado.Valor, pagamentoConsumido.Valor);
        Assert.Equal(pagamentoEnviado.DataPagamento, pagamentoConsumido.DataPagamento, TimeSpan.FromSeconds(1)); // Compara com tolerância de 1s para evitar erro de precisão
        Assert.Equal(pagamentoEnviado.Status, pagamentoConsumido.Status);
    }
}
