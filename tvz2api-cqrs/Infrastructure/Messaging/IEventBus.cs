using tvz2api_cqrs.Infrastructure.Events;

namespace tvz2api_cqrs.Infrastructure.Messaging
{
    public interface IEventBus
  {
    void Publish<T>(T @event) where T : IEvent;
    Task PublishAsync<T>(T @event) where T : IEvent;
  }
}
