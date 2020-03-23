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
    public KolegijSpecification(List<SmjerEnum> smjerIds = null, string name = null, int minEcts = 1, int maxEcts = 6, string isvu = null)
    {
      SmjerIds = smjerIds;
      Name = name;
      MinEcts = minEcts;
      MaxEcts = maxEcts;
      Isvu = isvu;
    }

    public List<SmjerEnum> SmjerIds { get; }
    public string Name { get; }
    public int MinEcts { get; }
    public int MaxEcts { get; }
    public string Isvu { get; }

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

        if (!string.IsNullOrWhiteSpace(Isvu))
        {
          predicate = predicate.And(t => t.Isvu == Isvu);
        }

        predicate = predicate.And(t => t.Ects >= MinEcts && t.Ects <= MaxEcts);

        return predicate.Expand();
      }
    }
  }
}