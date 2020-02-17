using tvz2api_cqrs.Implementation.Events;
using tvz2api_cqrs.Implementation.EventHandlers;
using System.Threading.Tasks;
using tvz2api_cqrs.Implementation.Messaging;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace tvz2api_cqrs.Implementation.Messaging
{
  public class EventBus : IEventBus
  {
    IServiceProvider _serviceProvider;

    public EventBus(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void Publish<T>(T @event) where T : IEvent
    {
      var handlers = _serviceProvider.GetServices<IEventHandler<T>>();

      foreach (var eventHandler in handlers)
      {
        eventHandler.Handle(@event);
      }
    }

    public async Task PublishAsync<T>(T @event) where T : IEvent
    {
      var handlers = _serviceProvider.GetServices<IEventHandlerAsync<T>>();

      foreach (var eventHandler in handlers)
      {
        await eventHandler.HandleAsync(@event);
      }
    }
  }
}
