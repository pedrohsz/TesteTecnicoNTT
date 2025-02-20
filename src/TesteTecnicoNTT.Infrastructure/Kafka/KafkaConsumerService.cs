using Microsoft.Extensions.Hosting;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IKafkaConsumer _kafkaConsumer;

        public KafkaConsumerService(IKafkaConsumer kafkaConsumer)
        {
            _kafkaConsumer = kafkaConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            Console.WriteLine("Iniciando Consumer do Kafka...");

            var clientesTask = _kafkaConsumer.ConsumirMensagensAsync("cliente-criado", stoppingToken);
            var pagamentosTask = _kafkaConsumer.ConsumirMensagensAsync("pagamento-criado", stoppingToken);

            await Task.WhenAll(clientesTask, pagamentosTask);
        }
    }
}
