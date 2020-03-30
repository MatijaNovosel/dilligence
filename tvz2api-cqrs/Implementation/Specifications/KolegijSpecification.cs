using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Infrastructure.Specifications;
using tvz2api_cqrs.Models;

namespace tvz2api_cqrs.Implementation.Specifications
{
  public class KolegijSpecification : ISpecification<Kolegij>
  {
    public KolegijSpecification(int? userId = null, List<SmjerEnum> smjerIds = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      SmjerIds = smjerIds;
      Name = name;
      Subscribed = subscribed;
      NonSubscribed = nonSubscribed;
      UserId = userId;
    }

    public List<SmjerEnum> SmjerIds { get; }
    public string Name { get; }
    public bool Subscribed { get; }
    public bool NonSubscribed { get; }
    public int? UserId { get; }

    public Expression<Func<Kolegij, bool>> Predicate
    {
      get
      {
        Expression<Func<Kolegij, bool>> predicate = t => true;

        if (SmjerIds != null && SmjerIds.Count() > 0)
        {
          /*

            Inace linija ide: predicate = predicate.And(t => SmjerIds.Any(x => (int)x == t.SmjerId));
            EF ne moze prevesti taj query u SQL zbog problema na EFCore 3.1.0 (https://github.com/dotnet/efcore/issues/17342).

          */
          predicate = predicate.And(t => SmjerIds.Select(x => (int)x).Contains((int)t.SmjerId));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Naziv.ToLower().Contains(Name.ToLower()));
        }

        // Ako je odabran ili Subscribed ili NonSubscribed, logicki XOR
        if (Subscribed != NonSubscribed)
        {
          if (Subscribed)
          {
            predicate = predicate.And(t => t.Pretplata.Any(x => x.KolegijId == t.Id && x.StudentId == UserId));
          }

          if (NonSubscribed)
          {
            predicate = predicate.And(t => !t.Pretplata.Any(x => x.KolegijId == t.Id && x.StudentId == UserId));
          }
        }

        return predicate.Expand();
      }
    }
  }
}