using System;
using tvz2api_cqrs.Infrastructure.Events;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Infrastructure.Messaging
{
  public interface IEventBus
  {
    void Publish<T>(T @event) where T : IEvent;
    Task PublishAsync<T>(T @event) where T : IEvent;
  }
}
