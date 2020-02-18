using tvz2api_cqrs.Infrastructure.Events;
using tvz2api_cqrs.Infrastructure.EventHandlers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace tvz2api_cqrs.Infrastructure.Messaging
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
