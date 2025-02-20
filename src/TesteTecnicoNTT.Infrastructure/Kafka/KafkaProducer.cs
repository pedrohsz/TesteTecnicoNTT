using Confluent.Kafka;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using System.Text.Json;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task EnviarMensagemAsync<T>(string topic, T mensagem)
        {
            try
            {
                string key = Guid.NewGuid().ToString();
                var jsonMensagem = JsonSerializer.Serialize(mensagem);

                await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = jsonMensagem });

                Log.Information("[KafkaProducer] Mensagem enviada com sucesso para o tópico **{Topic}** | Chave: {Key}",
                       topic, key);
            }
            catch (Exception ex)
            {
                // tratar erro
                Log.Error("[KafkaProducer] Erro ao enviar mensagem para o tópico **{Topic}** | Erro: {Erro}",
                    topic, ex.Message);
                throw;
            }
        }
    }
}
