using MediatR;

namespace TesteTecnicoNTT.Domain.Common
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
