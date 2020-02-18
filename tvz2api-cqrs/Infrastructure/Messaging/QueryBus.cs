using tvz2api_cqrs.Infrastructure.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace tvz2api_cqrs.Infrastructure.Messaging
{
  public class QueryBus : IQueryBus
  {
    private readonly IServiceProvider _serviceProvider;

    public QueryBus(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public TResult Execute<TResult>(IQuery<TResult> query)
    {
      var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
      dynamic handler = _serviceProvider.GetService(handlerType);

      if (handler == null)
      {
        throw new InvalidOperationException("No query handler registered");
      }

      return handler.Handle((dynamic)query);
    }

    public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
    {
      var handlerType = typeof(IQueryHandlerAsync<,>).MakeGenericType(query.GetType(), typeof(TResult));
      dynamic handler = _serviceProvider.GetService(handlerType);

      if (handler == null)
      {
        throw new InvalidOperationException("No query handler registered");
      }

      return await handler.HandleAsync((dynamic)query);
    }

    public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
      var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No query handler registered");
      }

      return handler.Handle(query);
    }

    public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
      var handler = _serviceProvider.GetService<IQueryHandlerAsync<TQuery, TResult>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No query handler registered");
      }

      return await handler.HandleAsync(query);
    }
  }
}
