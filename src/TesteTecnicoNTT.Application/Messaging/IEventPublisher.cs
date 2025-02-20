namespace TesteTecnicoNTT.Application.Messaging
{
    public interface IEventPublisher
    {
        Task PublishEventAsync(IEvent eventToPublish);
    }
}
