using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class NotificationQueryModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Description { get; set; }
    public string Course { get; set; }
    public string SubmittedBy { get; set; }
  }
}
