using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class DiscussionDTO
  {
    public int Id { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Content { get; set; }
    public int? CourseId { get; set; }
    public int? SubmittedById { get; set; }
    public string UserPictureBase64String { get; set; }
    public string SubmittedBy { get; set; }
  }

  public class DiscussionReplyDTO
  {
    public int Id { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Content { get; set; }
    public int SubmittedById { get; set; }
    public string SubmittedBy { get; set; }
    public string UserPicture { get; set; }
  }
}