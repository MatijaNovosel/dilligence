using tvz2api_cqrs.Models;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Infrastructure.Specifications;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.Specifications
{
  public class EmployeeSpecification : ISpecification<Zaposlenik>
  {
    public EmployeeSpecification(string name = null, string surname = null, List<OdjelEnum> odjelIds = null, List<ZaposljenjeEnum> zaposljenjeIds = null)
    {
      Name = name;
      Surname = surname;
      OdjelIds = odjelIds;
      ZaposljenjeIds = zaposljenjeIds;
    }

    public string Name { get; }
    public string Surname { get; }
    public List<OdjelEnum> OdjelIds { get; }
    public List<ZaposljenjeEnum> ZaposljenjeIds { get; }

    public Expression<Func<Zaposlenik, bool>> Predicate
    {
      get
      {
        Expression<Func<Zaposlenik, bool>> predicate = t => true;

        if (OdjelIds != null && OdjelIds.Count() > 0)
        {
          predicate = predicate.And(t => OdjelIds.Select(x => (int)x).Contains((int)t.OdjelId));
        }

        if (ZaposljenjeIds != null && ZaposljenjeIds.Count() > 0)
        {
          predicate = predicate.And(t => ZaposljenjeIds.Select(x => (int)x).Contains((int)t.VrstaZaposljenjaId));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Ime.ToLower().Contains(Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(Surname))
        {
          predicate = predicate.And(t => t.Prezime.ToLower().Contains(Surname.ToLower()));
        }

        return predicate.Expand();
      }
    }
  }
}
