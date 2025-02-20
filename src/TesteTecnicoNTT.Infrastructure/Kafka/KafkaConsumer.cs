using Confluent.Kafka;
using Serilog;
using System.Text.Json;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure.Kafka.Models;
using TesteTecnicoNTT.Infrastructure.Persistence.MongoDB;

namespace TesteTecnicoNTT.Infrastructure.Kafka
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly MongoDbContext _mongoDbContext;

        public KafkaConsumer(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task ConsumirMensagensAsync(string topic, CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "grupo-processamento",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                //EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var mensagem = consumer.Consume(cancellationToken);

                    if (mensagem != null)
                    {
                        Log.Information("[KafkaConsumer] Mensagem recebida do tópico **{Topic}** | Chave: {Key} | Payload: {Payload}",
                            topic, mensagem.Message.Key, mensagem.Message.Value);

                        // Uma melhor opção seria utilizar strategy e/ou configurar um consumer para cada tipo de mensagem
                        switch (topic)
                        {
                            case "cliente-criado":
                                await ProcessarClienteCriado(mensagem.Message.Value);
                                break;
                            case "pagamento-criado":
                                await ProcessarPagamentoCriado(mensagem.Message.Value);
                                break;
                        }

                        Log.Information("[KafkaConsumer] Mensagem do tópico **{Topic}** processada com sucesso.", topic);

                        //consumer.Commit(mensagem);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("[KafkaConsumer] Erro ao processar mensagem do Kafka no tópico **{Topic}** | Erro: {Erro}",
                        topic, ex.Message);
                }
            }
        }

        private async Task ProcessarClienteCriado(string mensagem)
        {
            try
            {
                var cliente = JsonSerializer.Deserialize<ClienteDto>(mensagem);
                var collection = _mongoDbContext.GetCollection<ClienteDto>("Clientes");
                await collection.InsertOneAsync(cliente);

                Log.Information("[KafkaConsumer] Cliente salvo no MongoDB: {@Cliente}", cliente);

            }
            catch (Exception ex)
            {
                Log.Error("[KafkaConsumer] Erro ao salvar Cliente no MongoDB | Erro: {Erro}", ex.Message);
                throw;
            }
        }

        private async Task ProcessarPagamentoCriado(string mensagem)
        {
            try
            {
                var pagamento = JsonSerializer.Deserialize<PagamentoDto>(mensagem);
                var collection = _mongoDbContext.GetCollection<PagamentoDto>("Pagamentos");
                await collection.InsertOneAsync(pagamento);
            }
            catch (Exception ex)
            {
                Log.Error("[KafkaConsumer] Erro ao salvar Pagamento no MongoDB | Erro: {Erro}", ex.Message);
                throw;
            }
        }
    }
}
