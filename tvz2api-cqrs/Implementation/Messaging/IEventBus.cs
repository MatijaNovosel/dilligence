using System;
using tvz2api_cqrs.Implementation.Events;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.Messaging
{
  public interface IEventBus
  {
    void Publish<T>(T @event) where T : IEvent;
    Task PublishAsync<T>(T @event) where T : IEvent;
  }
}
