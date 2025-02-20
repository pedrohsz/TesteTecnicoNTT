using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Enums;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure.Kafka;
using TesteTecnicoNTT.Infrastructure.Kafka.Models;
using TesteTecnicoNTT.Infrastructure.Persistence.MongoDB;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Repositories;

public class PostgresToMongoIntegrationTests
{
    private readonly PostgresDbContext _postgresDbContext;
    private readonly MongoDbContext _mongoDbContext;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IKafkaConsumer _kafkaConsumer;

    public PostgresToMongoIntegrationTests()
    {
        var postgresOptions = new DbContextOptionsBuilder<PostgresDbContext>()
            .UseNpgsql("Host=localhost;Database=Teste;Username=postgres;Password=teste")
            .Options;

        _postgresDbContext = new PostgresDbContext(postgresOptions);
        _clienteRepository = new ClienteRepository(_postgresDbContext);
        _pagamentoRepository = new PagamentoRepository(_postgresDbContext);
        _kafkaProducer = new KafkaProducer();

        var settingsMock = new Mock<IOptions<MongoDbSettings>>();
        settingsMock.Setup(s => s.Value).Returns(new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "Teste"
        });

        _mongoDbContext = new MongoDbContext(settingsMock.Object);
        _kafkaConsumer = new KafkaConsumer(_mongoDbContext);

        // Limpar os dados antes de cada teste para garantir um ambiente limpo
        _postgresDbContext.Database.EnsureDeleted();
        _postgresDbContext.Database.EnsureCreated();
    }

    // Teste de replicação de Cliente do PostgreSQL para o MongoDB
    [Fact]
    public async Task Deve_Replicar_Cliente_Do_Postgres_Para_Mongo()
    {
        // Arrange - Criar um Cliente no PostgreSQL
        var cliente = new Cliente(
            cpfCnpj: "12345678901",
            nome: "João Teste",
            numeroContrato: "CTR-3001",
            cidade: "São Paulo",
            estado: "SP",
            rendaBruta: 9000.00m
        );

        await _clienteRepository.AddAsync(cliente);

        // Enviar evento para o Kafka
        await _kafkaProducer.EnviarMensagemAsync("cliente-criado", cliente);

        // Act - Consumir a mensagem do Kafka e replicar para o MongoDB
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await _kafkaConsumer.ConsumirMensagensAsync("cliente-criado", cancellationToken);

        // Assert - Verificar se o Cliente foi salvo no MongoDB
        var collection = _mongoDbContext.GetCollection<ClienteDto>("Clientes");
        var clienteSalvo = await collection.Find(c => c.NumeroContrato == "CTR-3001").FirstOrDefaultAsync();

        Assert.NotNull(clienteSalvo);
        Assert.Equal(cliente.Nome, clienteSalvo.Nome);
    }

    // Teste de replicação de Pagamento do PostgreSQL para o MongoDB
    [Fact]
    public async Task Deve_Replicar_Pagamento_Do_Postgres_Para_Mongo()
    {
        // Arrange - Criar um Cliente no PostgreSQL
        var cliente = new Cliente(
            cpfCnpj: "98765432100",
            nome: "Maria Teste",
            numeroContrato: "CTR-4001",
            cidade: "Rio de Janeiro",
            estado: "RJ",
            rendaBruta: 12000.00m
        );

        await _clienteRepository.AddAsync(cliente);

        // Criar um Pagamento no PostgreSQL
        var pagamento = new Pagamento(
            clienteId: cliente.Id,
            numeroContrato: "CTR-4001",
            parcela: 1,
            valor: 3000.00m,
            dataPagamento: DateTime.UtcNow,
            status: StatusPagamento.AVencer
        );

        await _pagamentoRepository.AddAsync(pagamento);

        // Enviar evento para o Kafka
        await _kafkaProducer.EnviarMensagemAsync("pagamento-criado", pagamento);

        // Act - Consumir a mensagem do Kafka e replicar para o MongoDB
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await _kafkaConsumer.ConsumirMensagensAsync("pagamento-criado", cancellationToken);

        // Assert - Verificar se o Pagamento foi salvo no MongoDB
        var collection = _mongoDbContext.GetCollection<PagamentoDto>("Pagamentos");
        var pagamentoSalvo = await collection.Find(p => p.NumeroContrato == "CTR-4001").FirstOrDefaultAsync();

        Assert.NotNull(pagamentoSalvo);
        Assert.Equal(pagamento.Valor, pagamentoSalvo.Valor);
    }
}
