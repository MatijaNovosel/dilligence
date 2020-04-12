using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class ExamDetailsQueryModel
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public int? SubjectId { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }

  public class ExamAttemptQueryModel
  {
    public int Id { get; set; }
    public bool? Terminated { get; set; }
    public int? TimeLeft { get; set; }
    public ExamDTO Exam{ get; set; }
  }
}
