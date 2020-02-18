using tvz2api_cqrs.Infrastructure.Queries;

namespace tvz2api_cqrs.Infrastructure.Messaging
{
    public interface IQueryBus
  {
    TResult Execute<TResult>(IQuery<TResult> query);
    Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
  }
}
