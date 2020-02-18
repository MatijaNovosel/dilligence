using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tvz2api_cqrs.Infrastructure.Events;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Infrastructure.EventHandlers
{
  public interface IEventHandlerAsync<TEvent> where TEvent : IEvent
  {
    Task HandleAsync(TEvent handle);
  }

  public interface IEventHandler<TEvent> where TEvent : IEvent
  {
    void Handle(TEvent handle);
  }
}
