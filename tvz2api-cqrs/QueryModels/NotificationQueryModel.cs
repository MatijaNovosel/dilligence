using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.QueryModels
{
  public class NotificationQueryModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Description { get; set; }
    public string Course { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string Color { get; set; }
    public string SubmittedBy { get; set; }
    public int? CourseId { get; set; }
    public bool Archived { get; set; }
    public List<FileDTO> Attachments { get; set; }
    public int? SubmittedById { get; set; }
  }
}
