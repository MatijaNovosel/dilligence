using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.QueryModels
{
  public class CourseTaskQueryModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string CreatedBy { get; set; }
    public int? CourseId { get; set; }
    public List<FileDTO> Attachments { get; set; }
    public int MaximumGrade { get; set; }
  }
}
