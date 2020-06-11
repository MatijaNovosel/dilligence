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

  public class ExamUnfinishedDetailsQuery : IQuery<ExamUnfinishedDetailsQueryModel>
  {
    public ExamUnfinishedDetailsQuery(int id)
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

  public class ExamUnfinishedQuery : IQuery<List<UnfinishedExamDTO>>
  {
    public ExamUnfinishedQuery() { }
    public int UserId { get; set; }
    public int CourseId { get; set; }
  }

  public class ExamFinishedQuery : IQuery<List<FinishedExamDTO>>
  {
    public ExamFinishedQuery() { }
    public int UserId { get; set; }
    public int CourseId { get; set; }
  }

  public class ExamPreviewQuery : IQuery<ExamPreviewQueryModel>
  {
    public ExamPreviewQuery() { }
    public int ExamId { get; set; }
  }
}
