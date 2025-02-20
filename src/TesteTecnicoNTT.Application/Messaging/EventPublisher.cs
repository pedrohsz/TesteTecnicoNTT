using Microsoft.Extensions.Logging;

namespace TesteTecnicoNTT.Application.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(ILogger<EventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishEventAsync(IEvent eventToPublish)
        {
            _logger.LogInformation($"Event Published: {eventToPublish.EventName} at {eventToPublish.EventDate}");
            return Task.CompletedTask;
        }
    }
}
