using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class Notification
  {
    public Notification()
    {
      NotificationUserSeen = new HashSet<NotificationUserSeen>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public int? CourseId { get; set; }
    public int? SubmittedById { get; set; }

    public virtual Course Course { get; set; }
    public virtual User SubmittedBy { get; set; }
    public virtual ICollection<NotificationUserSeen> NotificationUserSeen { get; set; }
  }
}
