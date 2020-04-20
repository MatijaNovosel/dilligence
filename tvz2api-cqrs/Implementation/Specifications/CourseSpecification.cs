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
  public class CourseSpecification : ISpecification<Course>
  {
    public CourseSpecification(int? userId = null, List<SmjerEnum> smjerIds = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      SpecializationIds = smjerIds;
      Name = name;
      Subscribed = subscribed;
      NonSubscribed = nonSubscribed;
      UserId = userId;
    }

    public List<SmjerEnum> SpecializationIds { get; }
    public string Name { get; }
    public bool Subscribed { get; }
    public bool NonSubscribed { get; }
    public int? UserId { get; }


    public Expression<Func<Course, bool>> Predicate
    {
      get
      {
        Expression<Func<Course, bool>> predicate = t => true;

        if (SpecializationIds != null && SpecializationIds.Count() > 0)
        {

          // Inace linija ide: predicate = predicate.And(t => SmjerIds.Any(x => (int)x == t.SmjerId));
          // EF ne moze prevesti taj query u SQL zbog problema na EFCore 3.1.0 (https://github.com/dotnet/efcore/issues/17342).
          predicate = predicate.And(t => SpecializationIds.Select(x => (int)x).Contains((int)t.SpecializationId));
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
            predicate = predicate.And(t => t.Subscription.Any(x => x.CourseId == t.Id && x.UserId == UserId));
          }

          if (NonSubscribed)
          {
            predicate = predicate.And(t => !t.Subscription.Any(x => x.CourseId == t.Id && x.UserId == UserId));
          }
        }

        return predicate.Expand();
      }
    }
  }
}