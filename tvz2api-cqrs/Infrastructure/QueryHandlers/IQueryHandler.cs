using tvz2api_cqrs.Infrastructure.Queries;

namespace tvz2api_cqrs.Infrastructure.QueryHandlers
{
  public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
  {
    TResult Handle(TQuery query);
  }

  public interface IQueryHandlerAsync<TQuery, TResult> where TQuery : IQuery<TResult>
  {
    Task<TResult> HandleAsync(TQuery query);
  }
}
