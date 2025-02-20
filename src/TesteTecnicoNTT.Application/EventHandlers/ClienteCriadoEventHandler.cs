using MediatR;
using TesteTecnicoNTT.Domain.Events;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.EventHandlers
{
    public class ClienteCriadoEventHandler : INotificationHandler<ClienteCriadoEvent>
    {
        private readonly IKafkaProducer _kafkaProducer;

        public ClienteCriadoEventHandler(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task Handle(ClienteCriadoEvent notification, CancellationToken cancellationToken)
        {
            await _kafkaProducer.EnviarMensagemAsync("cliente-criado", notification);
        }
    }
}
