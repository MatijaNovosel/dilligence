using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class QuestionDTO
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int? TypeId { get; set; }
    public List<int> UserAnswers { get; set; }
    public List<AnswerDTO> Answers { get; set; }
  }

  public class CreateQuestionDTO
  {
    public string Title { get; set; }
    public string Content { get; set; }
    public int? TypeId { get; set; }
    public List<CreateAnswerDTO> Answers { get; set; }
  }
}
