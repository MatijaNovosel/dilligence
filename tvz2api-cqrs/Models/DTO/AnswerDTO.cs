using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class AnswerDTO
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public bool? Correct { get; set; }
  }
}
