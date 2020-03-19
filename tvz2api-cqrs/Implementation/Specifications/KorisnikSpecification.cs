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
  public class KorisnikSpecification : ISpecification<Korisnik>
  {
    public KorisnikSpecification(string name = null)
    {
      Name = name;
    }

    public string Name { get; }
    
    public Expression<Func<Korisnik, bool>> Predicate
    {
      get
      {
        Expression<Func<Korisnik, bool>> predicate = t => true;

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Username.ToLower().Contains(Name.ToLower()));
        }

        return predicate.Expand();
      }
    }
  }
}
