namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IKafkaConsumer
    {
        Task ConsumirMensagensAsync(string topic, CancellationToken cancellationToken);
    }
}
