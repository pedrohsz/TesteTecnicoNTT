using MediatR;
using TesteTecnicoNTT.Domain.Events;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Application.Pagamentos.EventHandlers
{
    public class PagamentoCriadoEventHandler : INotificationHandler<PagamentoCriadoEvent>
    {
        private readonly IKafkaProducer _kafkaProducer;

        public PagamentoCriadoEventHandler(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task Handle(PagamentoCriadoEvent notification, CancellationToken cancellationToken)
        {
            await _kafkaProducer.EnviarMensagemAsync("pagamento-criado", notification);
        }
    }
}
