using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class AnswerDTO
  {
    public int Value { get; set; }
    public string Label { get; set; }
  }

  public class CreateAnswerDTO
  {
    public string Content { get; set; }
    public bool Correct { get; set; } 
  }
}
