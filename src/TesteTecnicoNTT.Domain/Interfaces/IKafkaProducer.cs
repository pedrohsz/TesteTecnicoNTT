namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IKafkaProducer
    {
        Task EnviarMensagemAsync<T>(string topic, T mensagem);
    }
}
