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
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Specifications
{
  public class CourseTaskSpecification : ISpecification<CourseTask>
  {
    public CourseTaskSpecification(int courseId, string name)
    {
      CourseId = courseId;
      Name = name;
    }

    public string Name { get; }
    public int CourseId { get; }

    public Expression<Func<CourseTask, bool>> Predicate
    {
      get
      {
        Expression<Func<CourseTask, bool>> predicate = t => true;

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Title.ToLower().Contains(Name.ToLower()));
        }

        predicate = predicate.And(t => t.CourseId == CourseId);

        return predicate.Expand();
      }
    }
  }
}