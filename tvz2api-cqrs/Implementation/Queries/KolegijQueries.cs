using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

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

  public class KolegijDetailsQuery : IQuery<KolegijDetailsQueryModel>
  {
    public KolegijDetailsQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class KolegijSidebarQuery : IQuery<List<SidebarContentDTO>>
  {
    public KolegijSidebarQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class KolegijTotalQuery : IQuery<int>
  {
    public KolegijTotalQuery() { }
  }

  public class StudentKolegijQuery : IQuery<List<StudentQueryModel>>
  {
    public StudentKolegijQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class StudentKolegijTotalQuery : IQuery<int>
  {
    public StudentKolegijTotalQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }
}
