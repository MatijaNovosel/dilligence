using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.Implementation.Specifications;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class CourseTaskQuery : IQuery<List<CourseTaskQueryModel>>
  {
    public CourseTaskQuery() { }
    public CourseTaskQuery(CourseTaskSpecification specification)
    {
      Specification = specification;
    }
    public CourseTaskSpecification Specification { get; set; }
  }

  public class CourseTaskDetailsQuery : IQuery<CourseTaskQueryModel>
  {
    public CourseTaskDetailsQuery() { }
    public int Id { get; set; }
  }

  public class CourseTaskTotalQuery : IQuery<int>
  {
    public CourseTaskTotalQuery() { }
    public CourseTaskTotalQuery(CourseTaskSpecification specification)
    {
      Specification = specification;
    }
    public CourseTaskSpecification Specification { get; set; }
  }
}
