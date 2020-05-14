using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class CourseTaskQuery : IQuery<List<CourseTaskQueryModel>>
  {
    public CourseTaskQuery() { }
    public int CourseId { get; set; }
  }

  public class CourseTaskDetailsQuery : IQuery<CourseTaskQueryModel>
  {
    public CourseTaskDetailsQuery() { }
    public int Id { get; set; }
  }
}
