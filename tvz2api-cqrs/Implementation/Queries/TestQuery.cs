using G7.Common;
using G7.Domain.QueryModels.Imperios;
using G7.Domain.Specifications.Imperios;
using G7.Infrastructure.Queries;
using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Queries
{
  public class TestQuery : IQuery<List<TestQueryModel>>
  {
    public TestQuery(QueryOptions queryOptions)
    {
      QueryOptions = queryOptions;
    }
    public QueryOptions QueryOptions { get; set; }
  }
}
