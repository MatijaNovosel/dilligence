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
  public class CourseQuery : IQuery<List<CourseQueryModel>>
  {
    public CourseQuery() { }
    public CourseQuery(CourseSpecification specification)
    {
      Specification = specification;
    }
    public CourseQuery(QueryOptions queryOptions, CourseSpecification specification)
    {
      QueryOptions = queryOptions;
      Specification = specification;
    }
    public QueryOptions QueryOptions { get; set; }
    public CourseSpecification Specification { get; set; }
  }

  public class CourseDetailsQuery : IQuery<CourseDetailsQueryModel>
  {
    public CourseDetailsQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class CourseSidebarQuery : IQuery<List<SidebarContentDTO>>
  {
    public CourseSidebarQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class CourseTotalQuery : IQuery<int>
  {
    public CourseTotalQuery(CourseSpecification specification)
    {
      Specification = specification;
    }
    public CourseTotalQuery() { }
    public CourseSpecification Specification { get; set; }
  }

  public class StudentKolegijQuery : IQuery<List<StudentQueryModel>>
  {
    public StudentKolegijQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class StudentKolegijTotalQuery : IQuery<int>
  {
    public StudentKolegijTotalQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }
}