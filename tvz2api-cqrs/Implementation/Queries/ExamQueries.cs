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
  public class ExamInProgressDetailsQuery : IQuery<ExamAttemptDetailsQueryModel>
  {
    public ExamInProgressDetailsQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class ExamInProgressQuery : IQuery<List<ExamAttemptQueryModel>>
  {
    public ExamInProgressQuery(int userId)
    {
      UserId = userId;
    }
    public int UserId { get; set; }
  }
}
