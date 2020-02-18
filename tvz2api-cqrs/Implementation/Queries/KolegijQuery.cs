using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class KolegijQuery : IQuery<List<KolegijQueryModel>>
  {
    public KolegijQuery() { }
    public KolegijQuery(QueryOptions queryOptions)
    {
      QueryOptions = queryOptions;
    }
    public QueryOptions QueryOptions { get; set; }
  }

  public class KolegijTotalQuery : IQuery<int>
  {
    public KolegijTotalQuery() { }
  }
}
