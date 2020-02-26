using System;
using System.Linq.Expressions;

namespace tvz2api_cqrs.Infrastructure.Specifications
{
  public interface ISpecification<T>
  {
    Expression<Func<T, bool>> Predicate { get; }
  }
}
