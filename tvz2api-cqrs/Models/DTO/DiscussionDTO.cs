using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class DiscussionDTO
  {
    public int Id { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Content { get; set; }
    public string BackgroundColor { get; set; }
    public string TextColor { get; set; }
    public int? CourseId { get; set; }
    public int? SubmittedById { get; set; }
    public string UserPictureBase64String { get; set; }
    public string SubmittedBy { get; set; }
    public List<string> Images { get; set; }
    public List<FileDTO> Attachments { get; set; }
  }
}