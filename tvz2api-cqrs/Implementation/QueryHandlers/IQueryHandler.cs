using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.QueryHandlers
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
