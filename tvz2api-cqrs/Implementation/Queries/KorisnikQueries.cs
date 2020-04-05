using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using tvz2api_cqrs.Infrastructure.Specifications;
using tvz2api_cqrs.Implementation.Specifications;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class KorisnikQuery : IQuery<List<KorisnikQueryModel>>
  {
    public KorisnikQuery() { }
    public KorisnikQuery(KorisnikSpecification specification)
    {
      Specification = specification;
    }
    public KorisnikSpecification Specification { get; set; }
  }

  public class KorisnikTotalQuery : IQuery<int>
  {
    public KorisnikTotalQuery(KorisnikSpecification specification)
    {
      Specification = specification;
    }
    public KorisnikTotalQuery() { }
    public KorisnikSpecification Specification { get; set; }
  }

  public class KorisnikChatQuery : IQuery<List<KorisnikChatQueryModel>>
  {
    public KorisnikChatQuery() { }
    public int Id { get; set; }
  }

  public class KorisnikSettingsQuery : IQuery<KorisnikSettingsQueryModel>
  {
    public KorisnikSettingsQuery() { }
    public int Id { get; set; }
  }

  public class KorisnikPretplataQuery : IQuery<List<int>>
  {
    public KorisnikPretplataQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }
}
