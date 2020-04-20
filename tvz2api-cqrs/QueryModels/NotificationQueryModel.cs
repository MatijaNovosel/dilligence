using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class NotificationQueryModel
  {
    public int Id { get; set; }
    public string Naslov { get; set; }
    public DateTime? Datum { get; set; }
    public string Opis { get; set; }
    public int? CourseId { get; set; }
    public int? SubmittedById { get; set; }
  }
}
